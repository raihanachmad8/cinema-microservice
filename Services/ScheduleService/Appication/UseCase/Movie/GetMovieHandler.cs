// using ScheduleService.Application.Interfaces.Services;
// using ScheduleService.Application.Events.Requests;
// using ScheduleService.Application.Events.Responses;
//
// namespace ScheduleService.Application.UseCases.Movies
// {
//     public class GetMovieHandler : IMovieServiceClient
//     {
//         private readonly INatsRequester _requester;
//
//         public MovieServiceClient(INatsRequester requester)
//         {
//             _requester = requester;
//         }
//
//         public async Task<GetMovieResponse> GetMovieByIdAsync(GetMovieRequest request)
//         {
//             var movie = await _movieServiceClient.GetMovieByIdAsync(request.MovieId);
//             if (movie == null)
//             {
//                 return null;
//             }
//
//             return movie;
//         }
//     }
// }