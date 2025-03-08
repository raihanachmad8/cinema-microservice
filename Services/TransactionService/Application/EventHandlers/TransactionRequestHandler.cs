using TransactionService.Appication.Events.User;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Application.Events.Requests;
using TransactionService.Application.Interfaces.Messaging;
using TransactionService.Application.Events.Responses;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.EventHandlers
{
    public class TransactionRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public TransactionRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetTicket  Request
            _natsSubscriber.SubscribeAsync<GetTransactionRequest, TransactionResponse>("transaction.get", HandleGetTransactionRequest);

            _natsSubscriber.Subscribe<TransactionCreatedEvent>("transaction.created", HandleTransactionCreatedEvent);
            
            _natsSubscriber.Subscribe<TransactionCreatedPaymentEvent>("transaction.created.payment", HandleTransactionCreatedPaymentEvent);
        }
        

        private async Task<TransactionResponse?> HandleGetTransactionRequest(GetTransactionRequest request)
        {
            Console.WriteLine($"[NATS] Handling transaction.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var TransactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();

            try
            {
                var transaction = await TransactionRepository.GetByIdAsync(request.Id);
                if (transaction == null) return null;

                return new TransactionResponse
                {
                    Id = transaction.Id,
                    UserId = transaction.UserId,
                    PaymentMethod = transaction.PaymentMethod.ToString(),
                    PaymentStatus = transaction.PaymentStatus.ToString(),
                    TicketId = transaction.TicketId,
                    TotalAmount = transaction.TotalAmount,
                    TransactionDate = transaction.TransactionDate,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving Ticket: {ex.Message}");
                return null;
            }
        }

        private void HandleTransactionCreatedPaymentEvent(TransactionCreatedPaymentEvent eventData)
        {
            Console.WriteLine($"[NATS] Transaction created: {eventData.Id}, Id: {eventData.Id} {eventData.Amount}");
            // Logika tambahan untuk menangani Transaction yang baru dibuat
        }
        
        private void HandleTransactionCreatedEvent(TransactionCreatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Transaction created: {eventData.Id}, Id: {eventData.TicketId} {eventData.TotalAmount}");
            // Logika tambahan untuk menangani Transaction yang baru dibuat
        }
        
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