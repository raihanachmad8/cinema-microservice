using System.Text;
using System.Text.Json;
using NATS.Client;
using TicketService.Application.Interfaces.Messaging;

namespace TicketService.Infrastructure.Messaging
{
    public class NatsPublisher : INatsPublisher
    {
        private readonly IConnection _connection;

        public NatsPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync<T>(string subject, T message)
        {
            try
            {
                var json = JsonSerializer.Serialize(message);
                var data = Encoding.UTF8.GetBytes(json);
                _connection.Publish(subject, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS Publisher] Error publishing to {subject}: {ex.Message}");
            }
        }
    }
}