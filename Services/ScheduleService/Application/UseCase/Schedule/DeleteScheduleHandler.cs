using AutoMapper;
using ScheduleService.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using ScheduleService.Appication.Events.User;
using ScheduleService.Application.Interfaces.Messaging;
using ScheduleService.Application.Interfaces.Services;

namespace ScheduleService.Application.UseCases
{
    public class DeleteScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISerilog<DeleteScheduleHandler> _logger;
        private readonly INatsPublisher _natsPublisher;
        private readonly IMapper _mapper;

        public DeleteScheduleHandler(IScheduleRepository scheduleRepository, ISerilog<DeleteScheduleHandler> logger, INatsPublisher natsPublisher, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _natsPublisher = natsPublisher;
            _mapper = mapper;
        }

        public async Task Handle(int id)
        {
            _logger.LogInformation("Deleting Schedule with ID: {Id}", id);

            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
            {
                _logger.LogWarning("Schedule with ID {Id} not found", id);
                throw new KeyNotFoundException($"Schedule with ID {id} not found.");
            }

            await _scheduleRepository.DeleteAsync(id);
            await _natsPublisher.PublishAsync("schedule.deleted", _mapper.Map<ScheduleDeletedEvent>(schedule));
            _logger.LogInformation("Schedwule with ID {Id} deleted successfully", id);
        }
    }
}