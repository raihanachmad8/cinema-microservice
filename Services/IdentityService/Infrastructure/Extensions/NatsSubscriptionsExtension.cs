using IdentityService.Infrastructure.Messaging;

namespace IdentityService.Infrastructure.Extensions
{
    public static class NatsSubscriptionExtensions
    {
        public static void UseNatsSubscriptions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var movieRequestHandler = scope.ServiceProvider.GetRequiredService<UserRequestHandler>();
            movieRequestHandler.RegisterSubscribers();
        }
    }
}