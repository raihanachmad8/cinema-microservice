// NatsSubscriptionExtensions.cs
using IdentityService.Application.Events.Requests;
using IdentityService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using IdentityService.Infrastructure.Messaging;

namespace IdentityService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}