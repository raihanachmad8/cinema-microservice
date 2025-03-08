using AutoMapper;
using MovieService.Appication.Events.Movie;
using MovieService.Domain.Entities;
using MovieService.Application.DTOs.Responses;

namespace MovieService.Application.Mapper;

public class MappingMovieProfile : Profile
{
    public MappingMovieProfile()
    {
        CreateMap<Movie, MovieResponse>();
        
        CreateMap<Movie, MovieCreatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.DurationInMinutes));

        // Map Movie entity to MovieUpdatedEvent
        CreateMap<Movie, MovieUpdatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.DurationInMinutes));

        // Map Movie entity to MovieDeletedEvent
        CreateMap<Movie, MovieDeletedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}