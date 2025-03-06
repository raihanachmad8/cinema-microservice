using AutoMapper;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;

namespace ScheduleService.Application.UseCases
{
    public class GetSchedulesHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<GetSchedulesHandler> _logger;
        private readonly IMapper _mapper;

        public GetSchedulesHandler(IScheduleRepository scheduleRepository, ILogger<GetSchedulesHandler> logger, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ScheduleResponse>>> Handle(ScheduleQueryParams queryParams)
        {
            _logger.LogInformation(
                "Retrieving Schedules with MovieId: {MovieId}, StudioId: {StudioId}, OrderBy: {OrderBy}, Sort: {Sort}, Page: {Page}, PageSize: {PageSize}",
                queryParams.MovieId, queryParams.StudioId, queryParams.OrderBy, queryParams.Sort, queryParams.Page,
                queryParams.PageSize);

            Guid? movieId = string.IsNullOrEmpty(queryParams.MovieId) ? (Guid?)null : Guid.Parse(queryParams.MovieId);
            Guid? studioId = string.IsNullOrEmpty(queryParams.StudioId)
                ? (Guid?)null
                : Guid.Parse(queryParams.StudioId);
            var schedules = await _scheduleRepository.GetSchedulesAsync(movieId,
                studioId, queryParams.OrderBy, queryParams.Sort, queryParams.Page,
                queryParams.PageSize);


            
            return new Response<IEnumerable<ScheduleResponse>>().Ok(
                _mapper.Map<IEnumerable<ScheduleResponse>>(schedules.Schedules), "List of Schedules",
                schedules.Metadata);
        }
    }
}