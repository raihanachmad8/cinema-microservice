using Microsoft.EntityFrameworkCore;
using MovieService.Infrastructure.Persistence;

namespace MovieService.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseSettings:ConnectionString"];
        return services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}