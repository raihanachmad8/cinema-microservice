using IdentityService.Application.UseCase.Auth;
using IdentityService.Application.UseCase.Users;
using IdentityService.Application.UseCases;

namespace IdentityService.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();
        services.AddScoped<RegisterHandler>();
        services.AddScoped<RefreshTokenHandler>();
        services.AddScoped<LogoutHandler>();

        services.AddScoped<GetUserHandler>();
        services.AddScoped<UpdateUserHandler>();
        services.AddScoped<ChangePasswordHandler>();
        return services;
    }
}