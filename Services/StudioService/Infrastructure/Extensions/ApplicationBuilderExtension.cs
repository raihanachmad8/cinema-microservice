// NatsSubscriptionExtensions.cs
using StudioService.Application.Events.Requests;
using StudioService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using StudioService.Infrastructure.Messaging;

namespace StudioService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}