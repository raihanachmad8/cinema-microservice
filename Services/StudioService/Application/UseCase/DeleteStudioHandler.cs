using StudioService.Application.Interfaces.Repositories;
using StudioService.Application.Interfaces.Services;

namespace StudioService.Application.UseCases;

public class DeleteStudioHandler
{
    private readonly IStudioRepository _studioRepository;
    private readonly ISerilog<DeleteStudioHandler> _logger;

    public DeleteStudioHandler(IStudioRepository studioRepository, ISerilog<DeleteStudioHandler> logger)
    {
        _studioRepository = studioRepository;
        _logger = logger;
    }

    public async Task Handle(int id)
    {
        _logger.LogInformation("Deleting studio with ID: {Id}", id);

        var studio = await _studioRepository.GetByIdAsync(id);
        if (studio == null)
        {
            _logger.LogWarning("Studio with ID {Id} not found", id);
            throw new KeyNotFoundException($"Studio with ID {id} not found.");
        }

        await _studioRepository.DeleteAsync(id);
        _logger.LogInformation("Studio with ID {Id} deleted successfully", id);
    }
}