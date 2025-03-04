using AutoMapper;
using MovieService.Domain.Entities;
using MovieService.Application.DTOs.Responses;

namespace MovieService.Application.Mapper;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieResponse>();
    }
}