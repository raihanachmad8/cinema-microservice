using System;
using AutoMapper;
using MovieService.Application.DTOs.Responses;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Domain.Enums;

namespace MovieService.Application.UseCases
{
    public class FindByGenreHandler
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public FindByGenreHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<MovieResponse>>> Handle(string genreName)
        {
            if (!Enum.TryParse<Genre>(genreName, true, out var genre))
            {
                throw new ArgumentException("Invalid genre provided.");
            }

            var movies = await _movieRepository.GetByGenreAsync(genre);
            return new Response<IEnumerable<MovieResponse>>().Ok(_mapper.Map<IEnumerable<MovieResponse>>(movies));
        }
    }
}