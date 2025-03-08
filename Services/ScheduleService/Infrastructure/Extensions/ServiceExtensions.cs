using ScheduleService.Application.Interfaces.Services;
using ScheduleService.Infrastructure.Logging;
using Serilog;

namespace ScheduleService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ISerilog<>), typeof(SerilogLogger<>));
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/application-log.json", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        return services;
    }
}