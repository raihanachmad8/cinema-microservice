﻿using ScheduleService.Domain.Entities;

namespace ScheduleService.Application.DTOs.Responses;

public record SchedulePaginateResponse
{
    public IEnumerable<Schedule> Schedules { get; set; } = new List<Schedule>();
    public Metadata? Metadata { get; set; }
}