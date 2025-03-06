using IdentityService.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(typeof(MapperUserProfile))
            .AddDatabase(configuration)
            .AddNats(configuration)
            .AddRedisConnection(configuration)
            .AddServices()
            .AddAuthenticationExtensions(configuration)
            .AddFluentValidationServices()
            .AddUseCases()
            .AddRepositories();

        return services;
    }
}