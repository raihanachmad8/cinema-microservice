using AutoMapper;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

public class MappingProfileMapper : Profile
{
    public MappingProfileMapper()
    {
        CreateMap<Movie, MovieResponse>().ReverseMap(); 
        CreateMap<Studio, StudioResponse>().ReverseMap(); 
        CreateMap<Schedule, ScheduleResponse>() 
            .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => new MovieResponse
            {
                Id = src.MovieId,
                Title = src.Movie.Title,
                Genre = src.Movie.Genre.ToString(),
                DurationInMinutes = src.Movie.DurationInMinutes
            }))
            .ForMember(dest => dest.Studio, opt => opt.MapFrom(src => new StudioResponse()
            {
                Id = src.StudioId,
                Name = src.Studio.Name,
                Capacity = src.Studio.Capacity
            }))
            .ReverseMap();
    }
}