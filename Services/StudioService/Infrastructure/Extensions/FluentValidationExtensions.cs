using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Application.Validators;

namespace IdentityService.Infrastructure.Extensions
{
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
}
