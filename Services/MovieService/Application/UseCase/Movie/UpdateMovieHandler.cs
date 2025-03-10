﻿using AutoMapper;
using MovieService.Appication.Events.Movie;
using MovieService.Application.DTOs.Requests;
using MovieService.Application.DTOs.Responses;
using MovieService.Application.Interfaces.Messaging;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Application.Interfaces.Services;
using MovieService.Common.Exceptions;
using MovieService.Domain.Enums;

namespace MovieService.Application.UseCases;

public class UpdateMovieHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISerilog<UpdateMovieHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;

    public UpdateMovieHandler(IMovieRepository MovieRepository, ISerilog<UpdateMovieHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
    {
        _movieRepository = MovieRepository;
        _logger = logger;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
    }

    public async Task<Response<MovieResponse>> Handle(int id, MovieRequest request)
    {
        _logger.LogInformation("Updating Movie with ID: {Id}", id);

        var Movie = await _movieRepository.GetByIdAsync(id);
        if (Movie == null)
        {
            _logger.LogWarning("Movie with ID {Id} not found", id);
            throw new KeyNotFoundException($"Movie with ID {id} not found.");
        }

        var existingName = await _movieRepository.GetByTitleAsync(request.Title);
        if (existingName != null && existingName.Id != id)
        {
            _logger.LogWarning("Title {Title} already exists", request.Title);
            throw new ConflictException("Title already exists");
        }
        
        if (!Enum.TryParse<Genre>(request.Genre, true, out var genre))
        {
            throw new ArgumentException("Invalid genre provided.");
        }

        Movie.Title = request.Title;
        Movie.Description = request.Description;
        Movie.Genre = genre;
        Movie.UpdatedAt = DateTime.UtcNow;

        await _movieRepository.UpdateAsync(Movie);
        await _natsPublisher.PublishAsync("movie.updated", _mapper.Map<MovieUpdatedEvent>(Movie));

        return new Response<MovieResponse>().Ok(_mapper.Map<MovieResponse>(Movie), "Movie updated");
    }
}