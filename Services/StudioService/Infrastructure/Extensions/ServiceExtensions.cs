

using StudioService.Application.Interfaces.Services;
using StudioService.Infrastructure.Logging;

namespace StudioService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            return services;
        }
    }
}
