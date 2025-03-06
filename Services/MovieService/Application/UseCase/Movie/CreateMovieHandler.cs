using AutoMapper;
using MovieService.Appication.Events.Movie;
using MovieService.Application.DTOs.Requests;
using MovieService.Application.DTOs.Responses;
using MovieService.Application.Interfaces.Messaging;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Application.Interfaces.Services;
using MovieService.Common.Exceptions;
using MovieService.Domain.Entities;
using MovieService.Domain.Enums;

namespace MovieService.Application.UseCases;

public class CreateMovieHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISerilog<CreateMovieHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;

    public CreateMovieHandler(IMovieRepository MovieRepository, ISerilog<CreateMovieHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
    }

    public async Task<Response<MovieResponse>> Handle(MovieRequest request)
    {
        _logger.LogInformation("Creating Movie with title: {Title}", request.Title);


        // Cek conflic
        var existingMovie = await _movieRepository.GetByTitleAsync(request.Title);
        if (existingMovie != null && existingMovie.DeletedAt != null) throw new ConflictException("Title is already taken");
        
        if (!Enum.TryParse<Genre>(request.Genre, true, out var genre))
        {
            throw new ArgumentException("Invalid genre provided.");
        }

        var Movie = new Movie()
        {
            Title = request.Title,
            DurationInMinutes = request.DurationInMinutes,
            Genre = genre,
            Description = request.Description,
        };
        await _movieRepository.AddAsync(Movie);
        await _natsPublisher.PublishAsync("movie.created", _mapper.Map<MovieCreatedEvent>(Movie));

        return new Response<MovieResponse>().Ok(_mapper.Map<MovieResponse>(Movie), "Created Movie");
    }
}