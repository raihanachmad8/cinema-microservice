using Microsoft.EntityFrameworkCore;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseSettings:ConnectionString"];
        return services.AddDbContext<TicketDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}