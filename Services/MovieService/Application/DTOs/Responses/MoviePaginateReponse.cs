using MovieService.Domain.Entities;

namespace MovieService.Application.DTOs.Responses;

public record MoviePaginateResponse
{
    public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
    public Metadata Metadata { get; set; }
}