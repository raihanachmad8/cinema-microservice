using ScheduleService.Application.UseCases;

namespace ScheduleService.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateScheduleHandler>();
        services.AddScoped<GetSchedulesHandler>();
        services.AddScoped<UpdateScheduleHandler>();
        services.AddScoped<DeleteScheduleHandler>();
        return services;
    }
}