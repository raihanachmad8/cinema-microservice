using StackExchange.Redis;

namespace IdentityService.Infrastructure.Extensions;

public class RedisConnection
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; }
    public bool IsSSL { get; set; }
    public string Password { get; set; } = "";
}

public static class RedisServiceExtensions
{
    public static IServiceCollection AddRedisConnection(this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnection = new RedisConnection();
        configuration.GetSection("RedisConnection").Bind(redisConnection);

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { $"{redisConnection.Host}:{redisConnection.Port}" },
            AbortOnConnectFail = false,
            Ssl = redisConnection.IsSSL,
            Password = redisConnection.Password
        }));

        return services;
    }
}