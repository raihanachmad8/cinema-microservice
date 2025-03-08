using Microsoft.Extensions.DependencyInjection;

namespace TicketService.Infrastructure.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddSharedAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options => { });

        return services;
    }
}