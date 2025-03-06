using Microsoft.Extensions.DependencyInjection;

namespace MovieService.Infrastructure.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddSharedAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options => { });

        return services;
    }
}