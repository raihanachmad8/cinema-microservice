using TransactionService.Application.DTOs.Requests;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Application.Interfaces.Messaging;
using TransactionService.Domain.Entities;
using TransactionService.Common.Exceptions;
using AutoMapper;
using TransactionService.Appication.Events.User;
using TransactionService.Application.Events.Requests;
using TransactionService.Application.Events.Responses;
using TransactionService.Domain.Enums;

public class CreateTransactionHandler
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;
    private readonly INatsRequester _natsRequester;


    public CreateTransactionHandler(
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

    public async Task<Response<TransactionResponse>> Handle(int userId, TransactionRequest request)
    {
        var currentTime = DateTime.UtcNow;

        // Fetch the ticket ID
        var ticket =
            await _natsRequester.Request<GetTicketRequest, TicketResponse>("ticket.get",
                new GetTicketRequest(request.TicketId));
        if (ticket == null && ticket.UserId != userId)
        {
            throw new KeyNotFoundException("Ticket not found");
        }

        // Check existing transaction
        var existingTransaction = await _transactionRepository.GetByTicketIdAsync(request.TicketId);
        if (existingTransaction != null || existingTransaction.TransactionDate.AddMinutes(10) > currentTime &&
            existingTransaction.PaymentStatus != PaymentStatus.Successful)
        {
            throw new ConflictException("The transaction has already been created");
        }

        var schedule =
            await _natsRequester.Request<GetScheduleRequest, GetScheduleResponse>("schedule.get",
                new GetScheduleRequest(ticket.ScheduleId));

        var transaction = new Transaction
        {
            UserId = userId,
            TicketId = request.TicketId,
            PaymentStatus = PaymentStatus.Pending,
            TotalAmount = schedule.TicketPrice,
            TransactionDate = currentTime,
        };
        
        await _transactionRepository.AddAsync(transaction);
        await _natsPublisher.PublishAsync("transaction.created", _mapper.Map<TransactionCreatedEvent>(transaction));
        var transactionResponse = _mapper.Map<TransactionResponse>(transaction);
        transactionResponse.Ticket = _mapper.Map<TicketResponse>(ticket);

        return new Response<TransactionResponse>().Created(transactionResponse, "Transaction Created");
    }
}