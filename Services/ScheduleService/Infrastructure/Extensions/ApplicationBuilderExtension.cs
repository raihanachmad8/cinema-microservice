// NatsSubscriptionExtensions.cs
using ScheduleService.Application.Events.Requests;
using ScheduleService.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using ScheduleService.Infrastructure.Messaging;

namespace ScheduleService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationBuilderExtensions(this IApplicationBuilder app)
        {
            app.UseNatsSubscriptions();
        }
    }
}