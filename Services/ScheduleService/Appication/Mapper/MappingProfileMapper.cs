using AutoMapper;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

public class MappingProfileMapper : Profile
{
    public MappingProfileMapper()
    {
        CreateMap<Schedule, ScheduleResponse>();
    }
}