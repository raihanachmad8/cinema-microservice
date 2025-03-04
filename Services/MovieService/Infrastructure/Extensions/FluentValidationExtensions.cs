using FluentValidation;
using FluentValidation.AspNetCore;
using MovieService.Application.Validators;

namespace MovieService.Infrastructure.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<MovieRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<MovieQueryParamsValidator>();
        return services;
    }
}