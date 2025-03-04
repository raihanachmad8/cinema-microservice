using AutoMapper;
using StudioService.Domain.Entities;
using StudioService.Application.DTOs.Responses;

namespace StudioService.Application.Mapper;

public class StudioMappingProfile : Profile
{
    public StudioMappingProfile()
    {
        CreateMap<Studio, StudioResponse>();
    }
}