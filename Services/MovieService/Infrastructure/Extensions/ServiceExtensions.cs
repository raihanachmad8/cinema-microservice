using MovieService.Application.Interfaces.Services;
using MovieService.Infrastructure.Logging;

namespace MovieService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
        return services;
    }
}