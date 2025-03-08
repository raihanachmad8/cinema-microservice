using ScheduleService.Application.EventHandlers;
using ScheduleService.Infrastructure.Messaging;

namespace ScheduleService.Infrastructure.Extensions
{
    public static class NatsSubscriptionExtensions
    {
        public static void UseNatsSubscriptions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userRequestHandler = scope.ServiceProvider.GetRequiredService<ScheduleRequestHandler>();
            userRequestHandler.RegisterSubscribers();
        }
    }
}