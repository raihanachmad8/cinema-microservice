using AutoMapper;
using MovieService.Application.Interfaces.Messaging;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Application.Interfaces.Services;

namespace MovieService.Application.UseCases;

public class DeleteMovieHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISerilog<DeleteMovieHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;

    public DeleteMovieHandler(IMovieRepository MovieRepository, ISerilog<DeleteMovieHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
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
        await _natsPublisher.PublishAsync("movie.deleted", _mapper.Map<DeleteMovieHandler>(Movie));
        _logger.LogInformation("Movie with ID {Id} deleted successfully", id);
    }
}