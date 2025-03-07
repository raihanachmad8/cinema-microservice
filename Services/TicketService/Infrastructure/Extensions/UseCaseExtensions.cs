
using TicketService.Application.UseCases;

namespace TicketService.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {

        services.AddScoped<CreateTicketHandler>();
        services.AddScoped<GetTicketsHandler>();
        return services;
    }
}