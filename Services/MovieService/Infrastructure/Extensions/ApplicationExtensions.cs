using MovieService.Application.Mapper;
using MovieServiceService.Infrastructure.Extensions;

namespace MovieService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(typeof(MappingMovieProfile))
            .AddDatabase(configuration)
            .AddNats(configuration)
            .AddAuthenticationExtensions(configuration)
            .AddFluentValidationServices()
            .AddUseCases()
            .AddServices()
            .AddRepositories()
            ;

        return services;
    }
}