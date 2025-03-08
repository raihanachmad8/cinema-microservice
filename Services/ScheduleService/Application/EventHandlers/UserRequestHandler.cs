using ScheduleService.Appication.Events.User;
using ScheduleService.Application.Events.Requests;
using ScheduleService.Application.Interfaces.Messaging;
using ScheduleService.Application.Events.Responses;
using ScheduleService.Application.Interfaces.Repositories;

namespace ScheduleService.Application.EventHandlers
{
    public class ScheduleRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public ScheduleRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetSchedule  Request
            _natsSubscriber.SubscribeAsync<GetScheduleRequest, GetScheduleResponse>("schedule.get", HandleGetScheduleRequest);

            // Handler untuk ScheduleCreatedEvent
            _natsSubscriber.Subscribe<ScheduleCreatedEvent>("schedule.created", HandleScheduleCreatedEvent);

            // Handler untuk ScheduleUpdatedEvent
            _natsSubscriber.Subscribe<ScheduleUpdatedEvent>("schedule.updated", HandleScheduleUpdatedEvent);

            // Handler untuk ScheduleDeletedEvent
            _natsSubscriber.Subscribe<ScheduleDeletedEvent>("schedule.deleted", HandleScheduleDeletedEvent);

            Console.WriteLine("[NATS] ScheduleRequestHandler registered for schedule.get, schedule.created, schedule.updated, and schedule.deleted");
        }

        private async Task<GetScheduleResponse?> HandleGetScheduleRequest(GetScheduleRequest request)
        {
            Console.WriteLine($"[NATS] Handling schedule.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();

            try
            {
                var schedule = await scheduleRepository.GetByIdAsync(request.Id);
                if (schedule == null) return null;

                return new GetScheduleResponse
                {
                    Id = schedule.Id,
                    MovieId = schedule.MovieId,
                    StudioId = schedule.StudioId,
                    TicketPrice = schedule.TicketPrice,
                    StartDatetime = schedule.StartDatetime,
                    EndDatetime = schedule.EndDatetime,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving schedule: {ex.Message}");
                return null;
            }
        }

        private void HandleScheduleCreatedEvent(ScheduleCreatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Schedule created: {eventData.Id}, Name: {eventData.MovieId} {eventData.StudioId}");
            // Logika tambahan untuk menangani schedule yang baru dibuat
        }

        private void HandleScheduleUpdatedEvent(ScheduleUpdatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Schedule updated: {eventData.Id}, Name: {eventData.MovieId} {eventData.StudioId}");
            // Logika tambahan untuk menangani schedule yang diperbarui
        }

        private void HandleScheduleDeletedEvent(ScheduleDeletedEvent eventData)
        {
            Console.WriteLine($"[NATS] Schedule deleted: {eventData.Id}");
            // Logika tambahan untuk menangani schedule yang dihapus
        }
    }
}