using Microsoft.EntityFrameworkCore;
using TransactionService.Infrastructure.Persistence;

namespace TransactionService.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DatabaseSettings:ConnectionString"];
        return services.AddDbContext<TransactionDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}