// NatsSubscriptionExtensions.cs
using TicketService.Application.Events.Requests;
using TicketService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using TicketService.Infrastructure.Messaging;

namespace TicketService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}