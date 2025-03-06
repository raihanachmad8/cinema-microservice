// using ScheduleService.Application.Interfaces.Services;
// using ScheduleService.Application.Events.Requests;
// using ScheduleService.Application.Events.Responses;
//
// namespace ScheduleService.Application.UseCases.Movies
// {
//     public class GetMovieHandler : IScheduleServiceClient
//     {
//         private readonly INatsRequester _requester;
//
//         public ScheduleServiceClient(INatsRequester requester)
//         {
//             _requester = requester;
//         }
//
//         public async Task<GetMovieResponse> GetMovieByIdAsync(GetMovieRequest request)
//         {
//             var movie = await _ScheduleServiceClient.GetMovieByIdAsync(request.MovieId);
//             if (movie == null)
//             {
//                 return null;
//             }
//
//             return movie;
//         }
//     }
// }