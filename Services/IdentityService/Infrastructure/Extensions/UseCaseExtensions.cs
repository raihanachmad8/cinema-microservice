using IdentityService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<LoginHandler>();
            services.AddScoped<RegisterHandler>();
            services.AddScoped<RefreshTokenHandler>();
            services.AddScoped<LogoutHandler>();
            return services;
        }
    }
}
