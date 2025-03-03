using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DatabaseSettings:ConnectionString"];
            return services.AddDbContext<IdentityDBContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
