using ScheduleService.Infrastructure.Extensions;

namespace ScheduleService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDatabase(configuration)
            ;

        return services;
    }
}