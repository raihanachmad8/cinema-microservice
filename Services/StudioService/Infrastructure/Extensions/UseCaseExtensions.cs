using StudioService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace StudioService.Infrastructure.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateStudioHandler>();
            services.AddScoped<GetStudiosHandler>();
            services.AddScoped<UpdateStudioHandler>();
            services.AddScoped<DeleteStudioHandler>();
            return services;
        }
    }
}
