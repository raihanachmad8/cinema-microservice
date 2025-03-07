using TicketService.Application.EventHandlers;
using TicketService.Infrastructure.Messaging;

namespace TicketService.Infrastructure.Extensions
{
    public static class NatsSubscriptionExtensions
    {
        public static void UseNatsSubscriptions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userRequestHandler = scope.ServiceProvider.GetRequiredService<TicketRequestHandler>();
            userRequestHandler.RegisterSubscribers();
        }
    }
}