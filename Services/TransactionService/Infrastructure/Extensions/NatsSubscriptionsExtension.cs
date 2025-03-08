using TransactionService.Application.EventHandlers;
using TransactionService.Infrastructure.Messaging;

namespace TransactionService.Infrastructure.Extensions
{
    public static class NatsSubscriptionExtensions
    {
        public static void UseNatsSubscriptions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userRequestHandler = scope.ServiceProvider.GetRequiredService<TransactionRequestHandler>();
            userRequestHandler.RegisterSubscribers();
        }
    }
}