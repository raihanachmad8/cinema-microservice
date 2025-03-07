using FluentValidation;
using FluentValidation.AspNetCore;
using TicketService.Application.Validators;

namespace TicketService.Infrastructure.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<TicketRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<TicketQueryParamsValidator>();
        return services;
    }
}