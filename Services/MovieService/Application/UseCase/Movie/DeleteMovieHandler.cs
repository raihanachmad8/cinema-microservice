using MovieService.Application.Interfaces.Repositories;
using MovieService.Application.Interfaces.Services;

namespace MovieService.Application.UseCases;

public class DeleteMovieHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISerilog<DeleteMovieHandler> _logger;

    public DeleteMovieHandler(IMovieRepository MovieRepository, ISerilog<DeleteMovieHandler> logger)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
    }

    public async Task Handle(int id)
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