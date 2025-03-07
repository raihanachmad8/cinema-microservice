using TicketService.Application.Interfaces.Repositories;
using TicketService.Infrastructure.Presistence.Repositories;
using TicketService.Infrastructure.Repositories;

namespace TicketService.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();

        return services;
    }
}