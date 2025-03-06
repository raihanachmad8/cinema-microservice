using StudioService.Application.Interfaces.Repositories;
using StudioService.Infrastructure.Persistence.Repositories;

namespace StudioService.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudioRepository, StudioRepository>();

        return services;
    }
}