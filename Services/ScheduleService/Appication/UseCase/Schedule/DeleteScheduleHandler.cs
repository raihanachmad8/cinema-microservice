using ScheduleService.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ScheduleService.Application.UseCases
{
    public class DeleteScheduleHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<DeleteScheduleHandler> _logger;

        public DeleteScheduleHandler(IScheduleRepository scheduleRepository, ILogger<DeleteScheduleHandler> logger)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
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
            _logger.LogInformation("Schedwule with ID {Id} deleted successfully", id);
        }
    }
}