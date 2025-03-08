using IdentityService.Infrastructure.Extensions;
using TransactionService.Infrastructure.Extensions;

namespace TransactionService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(typeof(MappingTransactionProfile))
            .AddDatabase(configuration)
            .AddNats(configuration)
            .AddFluentValidationServices()
            .AddRedisConnection(configuration)
            .AddAuthenticationExtensions(configuration)
            .AddRepositories()
            .AddUseCases()
            .AddServices()
            ;

        return services;
    }
}