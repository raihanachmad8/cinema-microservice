using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NATS.Client;
using MovieService.Application.Interfaces.Messaging;

namespace MovieService.Infrastructure.Messaging
{
    public class NatsSubscriber : INatsSubscriber
    {
        private readonly IConnection _connection;

        public NatsSubscriber(IConnection connection)
        {
            _connection = connection;
        }

        public void Subscribe<T>(string subject, Action<T> handler)
        {
            var subscription = _connection.SubscribeAsync(subject);
            subscription.MessageHandler += (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Message.Data);
                var data = JsonSerializer.Deserialize<T>(json);
                if (data != null)
                {
                    handler(data);
                }
            };
            subscription.Start();
        }

        public void SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> handler)
        {
            _connection.SubscribeAsync(subject, async (sender, args) =>
            {
                try
                {
                    var requestJson = Encoding.UTF8.GetString(args.Message.Data);
                    var request = JsonSerializer.Deserialize<TRequest>(requestJson);
                    if (request == null || string.IsNullOrEmpty(args.Message.Reply)) return;

                    var response = await handler(request);

                    var responseJson = JsonSerializer.Serialize(response);
                    _connection.Publish(args.Message.Reply, Encoding.UTF8.GetBytes(responseJson));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NATS] Error in SubscribeAsync({subject}): {ex.Message}");
                }
            });
        }
    }
}