using MovieService.Application.DTOs.Responses;
using MovieService.Domain.Enums;

namespace MovieService.Application.UseCases
{
    public class GetAllGenresHandler
    {
        public Task<Response<IEnumerable<string>>> Handle()
        {
            var genre = Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => g.ToString())
                .ToList();
            
            return Task.FromResult(new Response<IEnumerable<string>>().Ok(genre));
        }
    }
}