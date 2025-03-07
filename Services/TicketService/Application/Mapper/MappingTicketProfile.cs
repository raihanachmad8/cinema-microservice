using AutoMapper;
using TicketService.Appication.Events.User;
using TicketService.Application.DTOs.Responses;
using TicketService.Domain.Entities;

public class MappingTicketProfile : Profile
{
    public MappingTicketProfile()
    {
        CreateMap<Ticket, TicketResponse>()
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat)); // Ensure this mapping exists
        CreateMap<Seat, SeatResponse>();

        CreateMap<Ticket, TicketCreatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.SeatId, opt => opt.MapFrom(src => src.SeatId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ReservedAt, opt => opt.MapFrom(src => src.ReservedAt));
        
        CreateMap<Ticket, TicketDeletedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}