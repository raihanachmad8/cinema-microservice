using StudioService.Infrastructure.Messaging;

namespace StudioService.Infrastructure.Extensions
{
    public static class NatsSubscriptionExtensions
    {
        public static void UseNatsSubscriptions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var studioRequestHandler = scope.ServiceProvider.GetRequiredService<StudioRequestHandler>();
            studioRequestHandler.RegisterSubscribers();
        }
    }
}