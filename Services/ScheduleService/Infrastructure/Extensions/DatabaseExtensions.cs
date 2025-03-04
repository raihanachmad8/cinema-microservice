using Microsoft.EntityFrameworkCore;
using ScheduleService.Infrastructure.Persistence;

namespace ScheduleService.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseSettings:ConnectionString"];
        return services.AddDbContext<ScheduleDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}