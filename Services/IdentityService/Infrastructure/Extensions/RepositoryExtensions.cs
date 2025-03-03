using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }
    }
}
