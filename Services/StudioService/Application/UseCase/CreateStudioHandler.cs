using AutoMapper;
using StudioService.Appication.Events.User;
using StudioService.Application.DTOs.Requests;
using StudioService.Application.DTOs.Responses;
using StudioService.Application.Interfaces.Messaging;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Application.Interfaces.Services;
using StudioService.Common.Exceptions;
using StudioService.Domain.Entities;

namespace StudioService.Application.UseCases;

public class CreateStudioHandler
{
    private readonly IStudioRepository _studioRepository;
    private readonly ISerilog<CreateStudioHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;

    public CreateStudioHandler(IStudioRepository studioRepository, ISerilog<CreateStudioHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
    {
        _studioRepository = studioRepository;
        _logger = logger;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
    }

    public async Task<Response<StudioResponse>> Handle(StudioRequest request)
    {
        _logger.LogInformation("Creating studio with name: {Name}", request.Name);


        // Cek conflic
        var existingStudio = await _studioRepository.GetByNameAsync(request.Name);
        if (existingStudio != null) throw new ConflictException("Name is already exists");

        var studio = new Studio()
        {
            Name = request.Name,
            Capacity = request.Capacity,
            AdditionalFacilities = request.AdditionalFacilities
        };
        await _studioRepository.AddAsync(studio);
        await _natsPublisher.PublishAsync("studio.created", _mapper.Map<StudioCreatedEvent>(studio));

        return new Response<StudioResponse>().Created(_mapper.Map<StudioResponse>(studio), "Created studio");
    }
}