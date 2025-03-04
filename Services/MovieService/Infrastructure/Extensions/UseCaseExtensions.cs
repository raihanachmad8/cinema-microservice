using MovieService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace MovieService.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateMovieHandler>();
        services.AddScoped<GetMoviesHandler>();
        services.AddScoped<UpdateMovieHandler>();
        services.AddScoped<DeleteMovieHandler>();
        services.AddScoped<GetAllGenresHandler>();
        services.AddScoped<FindByGenreHandler>();
        return services;
    }
}