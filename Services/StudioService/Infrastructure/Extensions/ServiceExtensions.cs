using Serilog;
using StudioService.Application.Interfaces.Services;
using StudioService.Infrastructure.Logging;

namespace StudioService.Infrastructure.Extensions;

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