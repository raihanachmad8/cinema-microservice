using AutoMapper;
using StudioService.Domain.Entities;
using StudioService.Application.DTOs.Responses;

namespace StudioService.Application.Mapper;

public class MappingStudioProfile : Profile
{
    public MappingStudioProfile()
    {
        CreateMap<Studio, StudioResponse>();
    }
}