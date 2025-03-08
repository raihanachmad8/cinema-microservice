using StackExchange.Redis;
using TicketService.Appication.Events.User;
using TicketService.Application.Events.Requests;
using TicketService.Application.Interfaces.Messaging;
using TicketService.Application.Events.Responses;
using TicketService.Application.Events.Ticket;
using TicketService.Application.Interfaces.Repositories;
using TicketService.Domain.Entities;
using TicketService.Domain.Enums;

namespace TicketService.Application.EventHandlers
{
    public class TicketRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public TicketRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetTicket  Request
            _natsSubscriber.SubscribeAsync<GetTicketRequest, GetTicketResponse>("ticket.get", HandleGetTicketRequest);

            _natsSubscriber.Subscribe<ScheduleCreatedEvent>("schedule.created", HandleScheduleCreatedEvent);

            _natsSubscriber.Subscribe<TransactionCreatedEvent>("transaction.created", HandleTransactionCreatedEvent);

            _natsSubscriber.Subscribe<TransactionCreatedPaymentEvent>("transaction.created.payment",
                HandleTransactionCreatedPaymentEvent);

            // // Handler untuk TicketCreatedEvent
            // _natsSubscriber.Subscribe<TicketCreatedEvent>("ticket.created", HandleTicketCreatedEvent);
            //
            // // Handler untuk TicketUpdatedEvent
            // _natsSubscriber.Subscribe<TicketUpdatedEvent>("ticket.updated", HandleTicketUpdatedEvent);
            //
            // // Handler untuk TicketDeletedEvent
            // _natsSubscriber.Subscribe<TicketDeletedEvent>("ticket.deleted", HandleTicketDeletedEvent);
            //
            // Console.WriteLine("[NATS] TicketRequestHandler registered for Ticket.get, Ticket.created, Ticket.updated, and Ticket.deleted");
        }

        private void HandleTransactionCreatedPaymentEvent(TransactionCreatedPaymentEvent paymentEvent)
        {
            // Use Task.Run to invoke the async logic inside the void method.
            Task.Run(async () => { await HandleTransactionCreatedPaymentEventAsync(paymentEvent); });
        }

        private async Task HandleTransactionCreatedPaymentEventAsync(TransactionCreatedPaymentEvent paymentEvent)
        {
            Console.WriteLine(
                $"[NATS] Handling payment for transaction {paymentEvent.Id}, updating ticket status to Confirmed");

            using var scope = _scopeFactory.CreateScope();
            var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();

            try
            {
                // Retrieve the ticket by the transaction ID (or another identifier)
                var ticket = await ticketRepository.GetTicketByIdWithSeatsAsync(paymentEvent.TicketId, paymentEvent.UserId);
                if (ticket == null)
                {
                    Console.WriteLine($"[NATS] Ticket not found for transaction {paymentEvent.TicketId}");
                    return;
                }

                // Update the status of the ticket to "Confirmed"
                ticket.Status = TicketStatus.Confirmed;
                ticket.Seat.IsAvailable = false;
                await ticketRepository.UpdateTicketAsync(ticket);

                Console.WriteLine($"[NATS] Ticket with ID {ticket.Id} confirmed for transaction {paymentEvent.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[NATS] Error updating ticket status for transaction {paymentEvent.Id}: {ex.Message}");
            }
        }

        private void HandleTransactionCreatedEvent(TransactionCreatedEvent paymentEvent)
        {
            // Use Task.Run to invoke the async logic inside the void method.
            Task.Run(async () => { await HandleTransactionCreatedEventAsync(paymentEvent); });
        }

        private async Task HandleTransactionCreatedEventAsync(TransactionCreatedEvent paymentEvent)
        {
            Console.WriteLine(
                $"[NATS] Handling payment for transaction {paymentEvent.Id}, updating ticket status to Confirmed");

            using var scope = _scopeFactory.CreateScope();
            var redisConnection = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            var redis = redisConnection.GetDatabase();
            var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
            
            var ticket = await ticketRepository.GetTicketByIdAsync(paymentEvent.TicketId);

            try
            {
                // Retrieve the ticket by the transaction ID (or another identifier)
                if (ticket == null)
                {
                    Console.WriteLine($"[NATS] Ticket not found for transaction {paymentEvent.Id}");
                    return;
                }

                // Update the status of the ticket to "Pending"
                var seatKey = $"seat:{ticket.SeatId}";
                await redis.StringSetAsync(seatKey, "pending", TimeSpan.FromMinutes(10));
                
                

                Console.WriteLine($"[NATS] Ticket with ID {ticket.Id} confirmed for transaction {paymentEvent.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[NATS] Error updating ticket status for transaction {paymentEvent.Id}: {ex.Message}");
            }
        }

        private async Task<GetTicketResponse?> HandleGetTicketRequest(GetTicketRequest request)
        {
            Console.WriteLine($"[NATS] Handling Ticket.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var TicketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();

            try
            {
                var Ticket = await TicketRepository.GetTicketByIdAsync(request.Id);
                if (Ticket == null) return null;

                return new GetTicketResponse
                {
                    Id = Ticket.Id,
                    Status = Ticket.Status.ToString(),
                    ReservedAt = Ticket.ReservedAt,
                    ScheduleId = Ticket.ScheduleId,
                    SeatId = Ticket.SeatId,
                    UserId = Ticket.UserId,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving Ticket: {ex.Message}");
                return null;
            }
        }

        private void HandleScheduleCreatedEvent(ScheduleCreatedEvent scheduleEvent)
        {
            // Use Task.Run to invoke the async logic inside the void method.
            Task.Run(async () => { await HandleScheduleCreatedEventAsync(scheduleEvent); });
        }

        private async Task HandleScheduleCreatedEventAsync(ScheduleCreatedEvent scheduleEvent)
        {
            Console.WriteLine($"[NATS] Handling schedule creation for Id: {scheduleEvent.Id}");

            using var scope = _scopeFactory.CreateScope();
            var seatRepository = scope.ServiceProvider.GetRequiredService<ISeatRepository>();
            var natsRequester = scope.ServiceProvider.GetRequiredService<INatsRequester>();

            try
            {
                var studios = await natsRequester.Request<object, GetStudioResponse>("studio.get",
                    new GetStudioRequest(scheduleEvent.StudioId));
                // Generate seat names based on capacity
                var seats = GenerateSeats(studios.Capacity);
                foreach (var seat in seats)
                {
                    await seatRepository.AddSeatAsync(seat);
                }

                Console.WriteLine($"[NATS] Seats created for schedule with Capacity: {studios.Capacity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error creating seats: {ex.Message}");
            }
        }

        private List<Seat> GenerateSeats(int capacity)
        {
            var seats = new List<Seat>();
            char row = 'A'; // Start with row A
            int seatCount = 10; // Number of seats per row

            for (int i = 0; i < capacity; i++)
            {
                if (i > 0 && i % seatCount == 0)
                {
                    row++; // Move to the next row after every 10 seats
                }

                // Create a new seat
                var seat = new Seat
                {
                    SeatNumber = $"{row}{(i % seatCount) + 1}", // Generate seat name (e.g., A1, A2, ..., B1, B2, ...)
                    IsAvailable = true, // Initially available
                    StudioId = 1 // Set the appropriate StudioId
                };

                seats.Add(seat);
            }

            return seats;
        }

        // private void HandleTicketCreatedEvent(TicketCreatedEvent eventData)
        // {
        //     Console.WriteLine($"[NATS] Ticket created: {eventData.Id}, Name: {eventData.MovieId} {eventData.StudioId}");
        //     // Logika tambahan untuk menangani Ticket yang baru dibuat
        // }
        //
        // private void HandleTicketUpdatedEvent(TicketUpdatedEvent eventData)
        // {
        //     Console.WriteLine($"[NATS] Ticket updated: {eventData.Id}, Name: {eventData.MovieId} {eventData.StudioId}");
        //     // Logika tambahan untuk menangani Ticket yang diperbarui
        // }
        //
        // private void HandleTicketDeletedEvent(TicketDeletedEvent eventData)
        // {
        //     Console.WriteLine($"[NATS] Ticket deleted: {eventData.Id}");
        //     // Logika tambahan untuk menangani Ticket yang dihapus
        // }
    }
}