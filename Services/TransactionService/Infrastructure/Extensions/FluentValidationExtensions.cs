using FluentValidation;
using FluentValidation.AspNetCore;
using TransactionService.Application.Validators;

namespace TransactionService.Infrastructure.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<TransactionRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<TransactionQueryParamsValidator>();
        services.AddValidatorsFromAssemblyContaining<TransactionPaymentRequestValidator>();
        return services;
    }
}