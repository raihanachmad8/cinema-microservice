using MovieService.Appication.Events.Movie;
using MovieService.Application.Events.Requests;
using MovieService.Application.Events.Responses;
using MovieService.Application.Interfaces.Messaging;
using MovieService.Application.Interfaces.Repositories;

namespace MovieService.Application.EventHandlers
{
    public class MovieRequestHandler
    {
        private readonly INatsSubscriber _natsSubscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public MovieRequestHandler(INatsSubscriber natsSubscriber, IServiceScopeFactory scopeFactory)
        {
            _natsSubscriber = natsSubscriber;
            _scopeFactory = scopeFactory;
        }

        public void RegisterSubscribers()
        {
            // Handler untuk GetMovie  Request
            _natsSubscriber.SubscribeAsync<GetMovieRequest, GetMovieResponse>("movie.get", HandleGetMovieRequest);

            // Handler untuk MovieCreatedEvent
            _natsSubscriber.Subscribe<MovieCreatedEvent>("movie.created", HandleMovieCreatedEvent);

            // Handler untuk MovieUpdatedEvent
            _natsSubscriber.Subscribe<MovieUpdatedEvent>("movie.updated", HandleMovieUpdatedEvent);

            // Handler untuk MovieDeletedEvent
            _natsSubscriber.Subscribe<MovieDeletedEvent>("movie.deleted", HandleMovieDeletedEvent);

            Console.WriteLine("[NATS] MovieRequestHandler registered for movie.get, movie.created, movie.updated, and movie.deleted");
        }

        private async Task<GetMovieResponse?> HandleGetMovieRequest(GetMovieRequest request)
        {
            Console.WriteLine($"[NATS] Handling movie.get for ID: {request.Id}");

            using var scope = _scopeFactory.CreateScope();
            var movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

            try
            {
                var movie = await movieRepository.GetByIdAsync(request.Id);
                if (movie == null) return null;

                return new GetMovieResponse
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Genre = movie.Genre.ToString(),
                    DurationInMinutes = movie.DurationInMinutes,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NATS] Error retrieving movie: {ex.Message}");
                return null;
            }
        }

        private void HandleMovieCreatedEvent(MovieCreatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Movie created: {eventData.Id}, Name: {eventData.Title}");
            // Logika tambahan untuk menangani movie yang baru dibuat
        }

        private void HandleMovieUpdatedEvent(MovieUpdatedEvent eventData)
        {
            Console.WriteLine($"[NATS] Movie updated: {eventData.Id}, Name: {eventData.Title}");
            // Logika tambahan untuk menangani movie yang diperbarui
        }

        private void HandleMovieDeletedEvent(MovieDeletedEvent eventData)
        {
            Console.WriteLine($"[NATS] Movie deleted: {eventData.Id}");
            // Logika tambahan untuk menangani movie yang dihapus
        }
    }
}