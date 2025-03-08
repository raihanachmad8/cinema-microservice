using AutoMapper;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Application.Events.Requests;
using TransactionService.Application.Events.Responses;
using TransactionService.Application.Interfaces.Messaging;
using TransactionService.Application.Interfaces.Repositories;

namespace TransactionService.Application.UseCases
{
    public class GetDetailTransactionHandler
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly INatsRequester _natsRequester;
        private readonly IMapper _mapper;

        public GetDetailTransactionHandler(
            ITransactionRepository transactionRepository,
            INatsRequester natsRequester,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _natsRequester = natsRequester;
            _mapper = mapper;
        }

        public async Task<Response<TransactionResponse>> Handle(int userId, int transactionId)
        {
            var transaction = await _transactionRepository.GetByUserIdAsync(transactionId, userId);
            if (transaction == null) throw new KeyNotFoundException("Transaction not found");
            var ticket = await _natsRequester.Request<GetTicketRequest, TicketResponse>("ticket.get",
                new GetTicketRequest(transaction.TicketId));
            var response = _mapper.Map<TransactionResponse>(transaction);
            response.Ticket = _mapper.Map<TicketResponse>(ticket);


            return new Response<TransactionResponse>().Ok(response, "Get detail transaction");
        }
    }
}