using FluentValidation;
using FluentValidation.AspNetCore;
using StudioService.Application.Validators;

namespace StudioService.Infrastructure.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<StudioRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<StudioQueryParamsValidator>();
        return services;
    }
}