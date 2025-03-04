using Microsoft.EntityFrameworkCore;
using StudioService.Infrastructure.Persistence;

namespace StudioService.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DatabaseSettings:ConnectionString"];
            return services.AddDbContext<StudioDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
