using AutoMapper;
using StudioService.Appication.Events.User;
using StudioService.Application.Interfaces.Messaging;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Application.Interfaces.Services;
using StudioService.Domain.Entities;

namespace StudioService.Application.UseCases;

public class DeleteStudioHandler
{
    private readonly IStudioRepository _studioRepository;
    private readonly ISerilog<DeleteStudioHandler> _logger;
    private readonly INatsPublisher _natsPublisher;
    private readonly IMapper _mapper;

    public DeleteStudioHandler(IStudioRepository studioRepository, ISerilog<DeleteStudioHandler> logger, INatsPublisher natsPublisher, IMapper mapper)
    {
        _studioRepository = studioRepository;
        _logger = logger;
        _natsPublisher = natsPublisher;
        _mapper = mapper;
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
        await _natsPublisher.PublishAsync("studio.deleted", _mapper.Map<StudioDeletedEvent>(studio));
        _logger.LogInformation("Studio with ID {Id} deleted successfully", id);
    }
}