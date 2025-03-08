using FluentValidation;
using FluentValidation.AspNetCore;
using ScheduleService.Application.Validators;

namespace ScheduleService.Infrastructure.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ScheduleRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ScheduleQueryParamsValidator>();
        return services;
    }
}