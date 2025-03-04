using AutoMapper;
using StudioService.Application.DTOs.Requests;
using StudioService.Application.DTOs.Responses;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Common.Exceptions;
using StudioService.Domain.Entities;

namespace StudioService.Application.UseCases
{
    public class CreateStudioHandler
    {
        private readonly IStudioRepository _studioRepository;
        private readonly ILogger<CreateStudioHandler> _logger;
        private readonly IMapper _mapper;

        public CreateStudioHandler(IStudioRepository studioRepository, ILogger<CreateStudioHandler> logger, IMapper mapper)
        {
            _studioRepository = studioRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<StudioResponse>> Handle(StudioRequest request)
        {
            _logger.LogInformation("Creating studio with name: {Name}", request.Name);

            
            // Cek conflic
            var existingStudio = await _studioRepository.GetByNameAsync(request.Name);
            if (existingStudio != null)
            {
                throw new ConflictException("Name is already taken");
            }
            
            var studio = new Studio()
            {
                Name = request.Name,
                Capacity = request.Capacity,
                AdditionalFacilities = request.AdditionalFacilities,
            };
            await _studioRepository.AddAsync(studio);

            return new Response<StudioResponse>().Ok(_mapper.Map<StudioResponse>(studio), "Created studio");
        }
    }
}