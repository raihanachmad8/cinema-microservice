using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Infrastructure.Presistence.Repositories;

namespace TransactionService.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}