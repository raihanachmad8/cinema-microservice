using IdentityService.Appication.Events.User;
using IdentityService.Application.Events.Requests;
using IdentityService.Application.Events.Responses;
using IdentityService.Application.Interfaces.Messaging;
using IdentityService.Application.Interfaces.Repositories;

namespace IdentityService.Application.EventHandlers
{
    public class UserRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public UserRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetUser  Request
            _natsSubscriber.SubscribeAsync<GetUserRequest, GetUserResponse>("user.get", HandleGetUserRequest);

            // Handler untuk UserCreatedEvent
            _natsSubscriber.Subscribe<UserCreatedEvent>("user.created", HandleUserCreatedEvent);

            // Handler untuk UserUpdatedEvent
            _natsSubscriber.Subscribe<UserUpdatedEvent>("user.updated", HandleUserUpdatedEvent);

            // Handler untuk UserDeletedEvent
            _natsSubscriber.Subscribe<UserDeletedEvent>("user.deleted", HandleUserDeletedEvent);

            Console.WriteLine("[NATS] UserRequestHandler registered for user.get, user.created, user.updated, and user.deleted");
        }

        private async Task<GetUserResponse?> HandleGetUserRequest(GetUserRequest request)
        {
            Console.WriteLine($"[NATS] Handling user.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            try
            {
                var user = await userRepository.GetByIdAsync(request.Id);
                if (user == null) return null;

                return new GetUserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                    Role = user.Role.ToString(),
                    PhoneNumber = user.PhoneNumber,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving user: {ex.Message}");
                return null;
            }
        }

        private void HandleUserCreatedEvent(UserCreatedEvent eventData)
        {
            Console.WriteLine($"[NATS] User created: {eventData.Id}, Name: {eventData.Name}, Email: {eventData.Email}");
            // Logika tambahan untuk menangani user yang baru dibuat
        }

        private void HandleUserUpdatedEvent(UserUpdatedEvent eventData)
        {
            Console.WriteLine($"[NATS] User updated: {eventData.Id}, Name: {eventData.Name}, Email: {eventData.Email}");
            // Logika tambahan untuk menangani user yang diperbarui
        }

        private void HandleUserDeletedEvent(UserDeletedEvent eventData)
        {
            Console.WriteLine($"[NATS] User deleted: {eventData.Id}");
            // Logika tambahan untuk menangani user yang dihapus
        }
    }
}