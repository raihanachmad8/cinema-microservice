using ScheduleService.Infrastructure.Extensions;

namespace ScheduleService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(typeof(MappingProfileMapper))
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