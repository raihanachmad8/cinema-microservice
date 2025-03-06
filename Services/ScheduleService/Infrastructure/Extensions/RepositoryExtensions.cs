using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Infrastructure.Persistence.Repositories;

namespace ScheduleService.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        return services;
    }
}