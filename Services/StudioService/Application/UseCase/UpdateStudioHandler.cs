using AutoMapper;
using StudioService.Application.DTOs.Requests;
using StudioService.Application.DTOs.Responses;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Common.Exceptions;

namespace StudioService.Application.UseCases
{
    public class UpdateStudioHandler
    {
        private readonly IStudioRepository _studioRepository;
        private readonly ILogger<UpdateStudioHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateStudioHandler(IStudioRepository studioRepository, ILogger<UpdateStudioHandler> logger, IMapper mapper)
        {
            _studioRepository = studioRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<StudioResponse>> Handle(Guid id, StudioRequest request)
        {
            _logger.LogInformation("Updating studio with ID: {Id}", id);

            var studio = await _studioRepository.GetByIdAsync(id);
            if (studio == null)
            {
                _logger.LogWarning("Studio with ID {Id} not found", id);
                throw new KeyNotFoundException($"Studio with ID {id} not found.");
            }
            
            var existingName = await _studioRepository.GetByNameAsync(studio.Name);
            if (existingName != null)
            {
                _logger.LogWarning("Name {Name} already exists", studio.Name);
                throw new ConflictException("Name is already taken.");
            }

            studio.Name = request.Name;
            studio.Capacity = request.Capacity;
            studio.AdditionalFacilities = request.AdditionalFacilities;
            studio.UpdatedAt = DateTime.UtcNow;

            await _studioRepository.UpdateAsync(studio);

            return new Response<StudioResponse>().Ok(_mapper.Map<StudioResponse>(studio), "Studio updated");
        }
    }
}