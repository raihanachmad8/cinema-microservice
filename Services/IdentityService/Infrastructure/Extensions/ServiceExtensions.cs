using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Application.Services;
using IdentityService.Infrastructure.Logging;
using IdentityService.Infrastructure.Security;
using Serilog;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ISerilog<>), typeof(SerilogLogger<>));
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/application-log.json", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        return services;
    }
}