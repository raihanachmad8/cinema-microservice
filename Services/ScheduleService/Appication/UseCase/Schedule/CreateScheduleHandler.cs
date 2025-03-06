﻿using AutoMapper;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Common.Exceptions;
using ScheduleService.Domain.Entities;

namespace ScheduleService.Application.UseCases
{
    public class CreateScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<CreateScheduleHandler> _logger;
        private readonly IMapper _mapper;

        public CreateScheduleHandler(IScheduleRepository scheduleRepository, ILogger<CreateScheduleHandler> logger,
            IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<ScheduleResponse>> Handle(ScheduleRequest request)
        {
            _logger.LogInformation("Creating Schedule for MovieId: {MovieId} at StudioId: {StudioId}", request.MovieId,
                request.StudioId);
            int duration = 120;
            int tolerance = 20;
            var existingSchedules =
                await _scheduleRepository.GetByShowTimeAsync(request.StartDatetime, request.StudioId, duration + tolerance);
            Console.WriteLine(existingSchedules.Count());

            if (existingSchedules.Count() > 0)
                throw new ConflictException("A schedule already exists for this time at the specified studio.");

            var schedule = new Schedule()
            {
                MovieId = request.MovieId,
                StudioId = request.StudioId,
                StartDatetime = request.StartDatetime,
                EndDatetime = request.StartDatetime.AddMinutes(duration),
                TicketPrice = request.TicketPrice
            };

            await _scheduleRepository.AddAsync(schedule);

            return new Response<ScheduleResponse>().Ok(_mapper.Map<ScheduleResponse>(schedule), "Created Schedule");
        }
    }
}