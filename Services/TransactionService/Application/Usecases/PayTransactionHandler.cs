using AutoMapper;
using TransactionService.Appication.Events.User;
using TransactionService.Application.DTOs.Requests;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Application.Events.Requests;
using TransactionService.Application.Interfaces.Messaging;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common.Exceptions;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Usecases
{
    public class PayTransactionHandler
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly INatsPublisher _natsPublisher;
        private readonly INatsRequester _natsRequester;

        public PayTransactionHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            INatsPublisher natsPublisher,
            INatsRequester natsRequester
        )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _natsPublisher = natsPublisher;
            _natsRequester = natsRequester;
        }

        public async Task<Response<TransactionResponse>> Handle(int userId, int transactionId, TransactionPaymentRequest request)
        {
            var currentTime = DateTime.UtcNow;

            // Fetch the transaction from the database
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found");
            }

            // Check if the transaction has already been paid or not
            if (transaction.PaymentStatus == PaymentStatus.Successful)
            {
                throw new ConflictException("Transaction has already been paid");
            }

            // Fetch the ticket associated with the transaction
            var ticket =
                await _natsRequester.Request<GetTicketRequest, TicketResponse>("ticket.get",
                    new GetTicketRequest(transaction.TicketId));
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket not found");
            }

            // Check if the payment amount is correct

            if (transaction.TotalAmount < request.Amount)
            {
                throw new InvalidOperationException("Insufficient payment amount");
            }

            // Update transaction status to Successful
            transaction.PaymentStatus = PaymentStatus.Successful;
            transaction.TransactionDate = currentTime;

            await _transactionRepository.UpdateAsync(transaction);

            // Publish an event indicating the payment was successful
            await _natsPublisher.PublishAsync("transaction.created.payment", _mapper.Map<TransactionCreatedEvent>(transaction));

            // Return the updated transaction information
            var transactionResponse = _mapper.Map<TransactionResponse>(transaction);
            transactionResponse.Ticket = _mapper.Map<TicketResponse>(ticket);

            return new Response<TransactionResponse>().Ok(transactionResponse, "Payment successful");
        }
    }
}