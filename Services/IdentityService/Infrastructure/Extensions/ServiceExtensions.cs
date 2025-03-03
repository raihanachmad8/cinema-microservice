
using IdentityService.Application.Interfaces.Services;
using IdentityService.Infrastructure.Logging;

namespace IdentityService.Infrastructure.Extensions
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
