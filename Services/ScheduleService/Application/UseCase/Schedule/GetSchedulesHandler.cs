using AutoMapper;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Application.DTOs.Requests;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Events.Requests;
using ScheduleService.Application.Events.Responses;
using ScheduleService.Application.Interfaces.Messaging;
using ScheduleService.Application.Interfaces.Services;

namespace ScheduleService.Application.UseCases
{
    public class GetSchedulesHandler
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISerilog<GetSchedulesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly INatsRequester _natsRequester;

        public GetSchedulesHandler(IScheduleRepository scheduleRepository, ISerilog<GetSchedulesHandler> logger,
            IMapper mapper, INatsRequester natsRequester)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
            _natsRequester = natsRequester;
        }

        public async Task<Response<IEnumerable<ScheduleResponse>>> Handle(ScheduleQueryParams queryParams)
        {
            _logger.LogInformation(
                "Retrieving Schedules with MovieId: {MovieId}, StudioId: {StudioId}, OrderBy: {OrderBy}, Sort: {Sort}, Page: {Page}, PageSize: {PageSize}",
                queryParams.MovieId, queryParams.StudioId, queryParams.OrderBy, queryParams.Sort, queryParams.Page,
                queryParams.PageSize);

            var schedules = await _scheduleRepository.GetSchedulesAsync(queryParams.MovieId,
                queryParams.StudioId, queryParams.OrderBy, queryParams.Sort, queryParams.Page,
                queryParams.PageSize);
            var scheduleResponses = new List<ScheduleResponse>();
            foreach (var schedule in schedules.Schedules)
            {
                // Make NATS requests to fetch Movie and Studio details
                var movieResponse = await _natsRequester.Request<GetMovieRequest, GetMovieResponse>(
                    "movie.get", new GetMovieRequest(schedule.MovieId));

                var studioResponse = await _natsRequester.Request<GetStudioRequest, GetStudioResponse>(
                    "studio.get", new GetStudioRequest(schedule.StudioId));

                // Directly use the NATS responses in the ScheduleResponse
                var scheduleResponse = _mapper.Map<ScheduleResponse>(schedule);
                scheduleResponse.Movie = movieResponse;  
                scheduleResponse.Studio = studioResponse;
                
                scheduleResponses.Add(scheduleResponse);
                    
            }


            return new Response<IEnumerable<ScheduleResponse>>().Ok(scheduleResponses, "List of Schedules",
                schedules.Metadata);
        }
    }
}