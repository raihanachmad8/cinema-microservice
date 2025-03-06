using AutoMapper;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Mapper;

public class MapperUserProfile : Profile
{
    public MapperUserProfile ()
    {
        CreateMap<User, UserResponse>();
    }
}