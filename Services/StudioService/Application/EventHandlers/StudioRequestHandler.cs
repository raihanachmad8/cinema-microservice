using StudioService.Application.Events.Requests;
using StudioService.Application.Events.Responses;
using StudioService.Application.Interfaces.Messaging;
using StudioService.Application.Interfaces.Repositories;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StudioService.Appication.Events.User;

namespace StudioService.Infrastructure.Messaging
{
    public class StudioRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public StudioRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetStudio Request
            _natsSubscriber.SubscribeAsync<GetStudioRequest, GetStudioResponse>("studio.get", HandleGetStudioRequest);

            // Handler untuk StudioCreatedEvent
            _natsSubscriber.Subscribe<StudioCreatedEvent>("studio.created", HandleStudioCreatedEvent);

            // Handler untuk StudioUpdatedEvent
            _natsSubscriber.Subscribe<StudioUpdatedEvent>("studio.updated", HandleStudioUpdatedEvent);

            // Handler untuk StudioDeletedEvent
            _natsSubscriber.Subscribe<StudioDeletedEvent>("studio.deleted", HandleStudioDeletedEvent);

            Console.WriteLine("[NATS] StudioRequestHandler registered for studio.get, studio.created, studio.updated, and studio.deleted");
        }

        private async Task<GetStudioResponse> HandleGetStudioRequest(GetStudioRequest request)
        {
            Console.WriteLine($"[NATS] Handling studio.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var studioRepository = scope.ServiceProvider.GetRequiredService<IStudioRepository>();

            try
            {
                var studio = await studioRepository.GetByIdAsync(request.Id);
                if (studio == null) return null;

                return new GetStudioResponse
                {
                    Id = studio.Id,
                    Name = studio.Name,
                    Capacity = studio.Capacity,
                    AdditionalFacilities = studio.AdditionalFacilities
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving studio: {ex.Message}");
                return null;
            }
        }

        private void HandleStudioCreatedEvent(StudioCreatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Studio created: {eventData.Id}, Name: {eventData.Name}, Capacity: {eventData.Capacity}, AdditionalFacilities: {eventData.AdditionalFacilities}");
            // Logika tambahan untuk menangani studio yang baru dibuat
        }

        private void HandleStudioUpdatedEvent(StudioUpdatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Studio updated: {eventData.Id}, Name: {eventData.Name}, Capacity: {eventData.Capacity}, AdditionalFacilities: {eventData.AdditionalFacilities}");
            // Logika tambahan untuk menangani studio yang diperbarui
        }

        private void HandleStudioDeletedEvent(StudioDeletedEvent eventData)
        {
            Console.WriteLine($"[NATS] Studio deleted: {eventData.Id}");
            // Logika tambahan untuk menangani studio yang dihapus
        }
    }
}