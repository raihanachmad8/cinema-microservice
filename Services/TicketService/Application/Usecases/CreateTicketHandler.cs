using System;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Requests;
using TicketService.Application.DTOs.Responses;
using TicketService.Application.Interfaces.Repositories;
using TicketService.Application.Interfaces.Messaging;
using TicketService.Domain.Entities;
using TicketService.Common.Exceptions;
using AutoMapper;
using StackExchange.Redis;
using TicketService.Appication.Events.User;
using TicketService.Domain.Enums;
public class CreateTicketHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;
    private readonly IConnectionMultiplexer _redis;

    private const int ReservationBlockDurationMinutes = 1; // Duration to block the seat

    public CreateTicketHandler(
        ITicketRepository ticketRepository,
        ISeatRepository seatRepository,
        IMapper mapper,
        INatsPublisher natsPublisher,
        IConnectionMultiplexer redis
    )
    {
        _ticketRepository = ticketRepository;
        _seatRepository = seatRepository;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
        _redis = redis;
    }

    public async Task<Response<TicketResponse>> Handle(int userId, TicketRequest request)
    {
        var currentTime = DateTime.UtcNow;

        // Fetch the seat by ID
        var seat = await _seatRepository.GetSeatByIdAsync(request.SeatId);
        if (seat == null)
            throw new KeyNotFoundException("Seat not found.");

        // Check if the seat is available in Redis
        var db = _redis.GetDatabase();
        var seatKey = $"seat:{request.SeatId}";

        // Check if the seat is currently reserved in Redis
        if (await db.StringGetAsync(seatKey) == "reserved")
        {
            throw new ConflictException("Seat is currently unavailable for reservation.");
        }

        // Check if there is an existing ticket for the same seat and schedule
        var existingTicket = await _ticketRepository.GetTicketBySeatAndScheduleAsync(request.SeatId, request.ScheduleId);
        if (existingTicket != null && existingTicket.Status == TicketStatus.Confirmed)
        {
            throw new ConflictException("A confirmed ticket already exists for this seat.");
        }

        // Block the seat by setting it in Redis with an expiration time
        await db.StringSetAsync(seatKey, "reserved", TimeSpan.FromMinutes(ReservationBlockDurationMinutes));

        // Create the ticket
        var ticket = new Ticket
        {
            ScheduleId = request.ScheduleId,
            UserId = userId,
            SeatId = request.SeatId,
            Status = TicketStatus.Reserved, // Set the status to Reserved
            ReservedAt = currentTime
        };

        // Add the ticket to the repository
        await _ticketRepository.AddTicketAsync(ticket);

        // Optionally, publish an event to notify other services
        await _natsPublisher.PublishAsync("ticket.created", _mapper.Map<TicketCreatedEvent>(ticket));

        return new Response<TicketResponse>().Created(_mapper.Map<TicketResponse>(ticket), "Ticket created successfully.");
    }
}