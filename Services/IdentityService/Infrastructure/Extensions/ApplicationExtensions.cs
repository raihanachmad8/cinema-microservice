using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddDatabase(configuration)
                .AddRedisConnection(configuration)
                .AddAuthenticationExtensions(configuration)
                .AddFluentValidationServices()
                .AddUseCases()
                .AddServices()
                .AddRepositories();

            return services;
        }
    }
}
