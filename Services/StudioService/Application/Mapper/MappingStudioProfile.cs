using AutoMapper;
using StudioService.Appication.Events.User;
using StudioService.Domain.Entities;
using StudioService.Application.DTOs.Responses;

namespace StudioService.Application.Mapper;

public class MappingStudioProfile : Profile
{
    public MappingStudioProfile()
    {
        CreateMap<Studio, StudioResponse>();
        
        CreateMap<Studio, StudioCreatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.AdditionalFacilities, opt => opt.MapFrom(src => src.AdditionalFacilities));

        CreateMap<Studio, StudioUpdatedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.AdditionalFacilities, opt => opt.MapFrom(src => src.AdditionalFacilities));

        CreateMap<Studio, StudioDeletedEvent>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}