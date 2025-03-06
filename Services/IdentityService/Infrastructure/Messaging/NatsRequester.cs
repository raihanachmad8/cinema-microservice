using NATS.Client;
using System.Text;
using System.Text.Json;
using IdentityService.Application.Interfaces.Messaging;

namespace IdentityService.Infrastructure.Messaging
{
    public class NatsRequester : INatsRequester
    {
        private readonly IConnection _connection;

        public NatsRequester(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string subject, TRequest request)
        {
            var requestJson = JsonSerializer.Serialize(request);
            var reply = _connection.Request(subject, Encoding.UTF8.GetBytes(requestJson));
            var responseJson = Encoding.UTF8.GetString(reply.Data);
            return JsonSerializer.Deserialize<TResponse>(responseJson);
        }
    }
}