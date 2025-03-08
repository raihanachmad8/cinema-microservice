using AutoMapper;
using ScheduleService.Appication.Events.User;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Interfaces.Messaging;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Application.Interfaces.Services;
using ScheduleService.Common.Exceptions;

namespace ScheduleService.Application.UseCases
{
    public class UpdateScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISerilog<UpdateScheduleHandler> _logger;
        private readonly IMapper _mapper;
        private readonly INatsPublisher _natsPublisher;

        public UpdateScheduleHandler(IScheduleRepository scheduleRepository, ISerilog<UpdateScheduleHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
            _natsPublisher = natsPublisher;
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
            if (existingSchedule.Count() > 0 && existingSchedule.Where(i => i.StudioId == request.StudioId).Count() == 0)
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
            await _natsPublisher.PublishAsync("schedule.updated", _mapper.Map<ScheduleUpdatedEvent>(schedule));

            return new Response<ScheduleResponse>().Ok(_mapper.Map<ScheduleResponse>(schedule), "Schedule updated");
        }
    }
}