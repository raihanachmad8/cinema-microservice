using StudioService.Application.Mapper;

namespace StudioService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(typeof(StudioMappingProfile))
            .AddAuthenticationExtensions(configuration)
            .AddFluentValidationServices()
            .AddDatabase(configuration)
            .AddUseCases()
            .AddServices()
            .AddRepositories()
            ;

        return services;
    }
}