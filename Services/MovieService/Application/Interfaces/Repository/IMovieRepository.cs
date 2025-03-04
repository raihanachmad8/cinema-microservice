using MovieService.Application.DTOs.Responses;
using MovieService.Domain.Entities;

namespace MovieService.Application.Interfaces.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id);
    Task<Movie?> GetByTitleAsync(string title);
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
    Task AddAsync(Movie movie);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(Guid id);
    Task<MoviePaginateResponse> GetMoviesAsync(string search, string orderBy, string? sort, int page, int pageSize);
}