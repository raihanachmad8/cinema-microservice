using AutoMapper;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Common.Exceptions;

namespace ScheduleService.Application.UseCases
{
    public class UpdateScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<UpdateScheduleHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateScheduleHandler(IScheduleRepository scheduleRepository, ILogger<UpdateScheduleHandler> logger, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<ScheduleResponse>> Handle(int  id, ScheduleRequest request)
        {
            _logger.LogInformation("Updating Schedule with ID: {Id}", id);

            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
            {
                _logger.LogWarning("Schedule with ID {Id} not found", id);
                throw new KeyNotFoundException($"Schedule with ID {id} not found.");
            }

            int duration = 120;
            // Cek konflik, misalnya jika jadwal sudah ada untuk waktu yang sama di studio yang sama
            var existingSchedule = await _scheduleRepository.GetByShowTimeAsync(request.StartDatetime, request.StudioId, duration);
            if (existingSchedule.Count() > 0)
            {
                _logger.LogWarning("A schedule already exists for this time at the specified studio.");
                throw new ConflictException("A schedule already exists for this time at the specified studio.");
            }

            schedule.MovieId = request.MovieId;
            schedule.StudioId = request.StudioId;
            schedule.StartDatetime = request.StartDatetime;
            schedule.EndDatetime = request.StartDatetime.AddMinutes(duration);
            schedule.TicketPrice = request.TicketPrice;

            await _scheduleRepository.UpdateAsync(schedule);

            return new Response<ScheduleResponse>().Ok(_mapper.Map<ScheduleResponse>(schedule), "Schedule updated");
        }
    }
}