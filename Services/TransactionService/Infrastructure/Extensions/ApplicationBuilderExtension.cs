// NatsSubscriptionExtensions.cs
using TransactionService.Application.Events.Requests;
using TransactionService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Infrastructure.Messaging;

namespace TransactionService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}