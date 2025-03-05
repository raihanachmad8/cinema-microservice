using AutoMapper;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Common.Exceptions;
using ScheduleService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ScheduleService.Application.UseCases
{
    public class CreateScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<CreateScheduleHandler> _logger;
        private readonly IMapper _mapper;

        public CreateScheduleHandler(IScheduleRepository scheduleRepository, ILogger<CreateScheduleHandler> logger, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<ScheduleResponse>> Handle(ScheduleRequest request)
        {
            _logger.LogInformation("Creating Schedule for MovieId: {MovieId} at StudioId: {StudioId}", request.MovieId, request.StudioId);

            var existingSchedule = await _scheduleRepository.GetByShowTimeAsync(request.ShowTime, request.StudioId);
            Console.WriteLine($"{request.ShowTime.Date} - {request.StudioId} | ${existingSchedule.Count()}");
            if (existingSchedule.Count() > 0 ) throw new ConflictException("A schedule already exists for this time at the specified studio.");

            var schedule = new Schedule()
            {
                MovieId = request.MovieId,
                StudioId = request.StudioId,
                ShowTime = request.ShowTime,
                TicketPrice = request.TicketPrice
            };

            await _scheduleRepository.AddAsync(schedule);

            return new Response<ScheduleResponse>().Ok(_mapper.Map<ScheduleResponse>(schedule), "Created Schedule");
        }
    }
}