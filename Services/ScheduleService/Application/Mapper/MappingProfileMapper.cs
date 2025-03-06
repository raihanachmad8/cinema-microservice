using AutoMapper;
using ScheduleService.Appication.Events.User;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

public class MappingProfileMapper : Profile
{
    public MappingProfileMapper()
    {
        CreateMap<Schedule, ScheduleResponse>();

        CreateMap<Schedule, ScheduleCreatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
            .ForMember(dest => dest.StudioId, opt => opt.MapFrom(src => src.StudioId))
            .ForMember(dest => dest.TicketPrice, opt => opt.MapFrom(src => src.TicketPrice))
            .ForMember(dest => dest.StartDatetime, opt => opt.MapFrom(src => src.StartDatetime))
            .ForMember(dest => dest.EndDatetime, opt => opt.MapFrom(src => src.EndDatetime));
        
        CreateMap<Schedule, ScheduleUpdatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
            .ForMember(dest => dest.StudioId, opt => opt.MapFrom(src => src.StudioId))
            .ForMember(dest => dest.TicketPrice, opt => opt.MapFrom(src => src.TicketPrice))
            .ForMember(dest => dest.StartDatetime, opt => opt.MapFrom(src => src.StartDatetime))
            .ForMember(dest => dest.EndDatetime, opt => opt.MapFrom(src => src.EndDatetime));
        
        CreateMap<Schedule, ScheduleDeletedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

    }
}