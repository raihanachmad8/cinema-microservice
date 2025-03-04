using IdentityService.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace StudioService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddFluentValidationServices()
                .AddDatabase(configuration);

            return services;
        }
    }
}
