using ScheduleService.Application.Interfaces.Services;
using ScheduleService.Infrastructure.Logging;

namespace ScheduleService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
        return services;
    }
}