// NatsSubscriptionExtensions.cs
using MovieService.Application.Events.Requests;
using MovieService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MovieService.Infrastructure.Messaging;

namespace MovieService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}