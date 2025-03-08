using NATS.Client;
using StudioService.Application.Interfaces.Messaging;
using StudioService.Infrastructure.Messaging;

namespace StudioService.Infrastructure.Extensions
{
    public static class NatsExtensions
    {
        public static IServiceCollection AddNats(this IServiceCollection services, IConfiguration configuration)
        {
            var natsUrl = configuration["NatsSettings:Url"];

            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = natsUrl;

            var connection = new ConnectionFactory().CreateConnection(options);
            
            services.AddSingleton<IConnection>(connection);

            services.AddScoped<INatsRequester, NatsRequester>();
            services.AddScoped<INatsPublisher, NatsPublisher>();
            services.AddScoped<INatsSubscriber, NatsSubscriber>();
            
            services.AddScoped<StudioRequestHandler>();

            return services;
        }
    }
}