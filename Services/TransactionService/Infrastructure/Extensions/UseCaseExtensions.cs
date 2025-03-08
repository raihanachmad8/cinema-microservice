
using TransactionService.Application.Usecases;
using TransactionService.Application.UseCases;

namespace TransactionService.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {

        services.AddScoped<CreateTransactionHandler>();
        services.AddScoped<GetTransactionHandler>();
        services.AddScoped<GetDetailTransactionHandler>();
        services.AddScoped<GetPaymentHandler>();
        services.AddScoped<PayTransactionHandler>();
        return services;
    }
}