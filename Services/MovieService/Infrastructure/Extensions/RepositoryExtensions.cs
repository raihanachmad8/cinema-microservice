using MovieService.Application.Interfaces.Repositories;
using MovieService.Infrastructure.Persistence.Repositories;

namespace MovieServiceService.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }
}