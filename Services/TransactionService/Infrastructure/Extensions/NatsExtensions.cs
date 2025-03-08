using TransactionService.Application.EventHandlers;
using NATS.Client;
using TransactionService.Application.Interfaces.Messaging;
using TransactionService.Infrastructure.Messaging;

namespace TransactionService.Infrastructure.Extensions
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
            
            services.AddScoped<TransactionRequestHandler>();

            return services;
        }
    }
}