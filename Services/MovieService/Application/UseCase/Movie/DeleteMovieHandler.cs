using MovieService.Application.Interfaces.Repositories;

namespace MovieService.Application.UseCases;

public class DeleteMovieHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<DeleteMovieHandler> _logger;

    public DeleteMovieHandler(IMovieRepository MovieRepository, ILogger<DeleteMovieHandler> logger)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
    }

    public async Task Handle(Guid id)
    {
        _logger.LogInformation("Deleting Movie with ID: {Id}", id);

        var Movie = await _movieRepository.GetByIdAsync(id);
        if (Movie == null)
        {
            _logger.LogWarning("Movie with ID {Id} not found", id);
            throw new KeyNotFoundException($"Movie with ID {id} not found.");
        }

        await _movieRepository.DeleteAsync(id);
        _logger.LogInformation("Movie with ID {Id} deleted successfully", id);
    }
}