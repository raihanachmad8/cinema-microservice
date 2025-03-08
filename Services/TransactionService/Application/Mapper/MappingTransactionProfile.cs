using AutoMapper;
using TransactionService.Appication.Events.User;
using TransactionService.Application.DTOs.Responses;
using TransactionService.Domain.Entities;

public class MappingTransactionProfile : Profile
{
    public MappingTransactionProfile()
    {
        CreateMap<Transaction, TransactionResponse>();
        

        CreateMap<Transaction, TransactionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));

        // Pemetaan antara entitas Transaction dan TransactionCreatedEvent
        CreateMap<Transaction, TransactionCreatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));

        // Pemetaan antara entitas Transaction dan TransactionDeletedEvent
        CreateMap<Transaction, TransactionDeletedEvent>();
    }
}