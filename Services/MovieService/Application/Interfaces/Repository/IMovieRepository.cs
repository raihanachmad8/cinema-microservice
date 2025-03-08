using MovieService.Application.DTOs.Responses;
using MovieService.Domain.Entities;
using MovieService.Domain.Enums;

namespace MovieService.Application.Interfaces.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie?> GetByTitleAsync(string title);
    Task<List<Movie>> GetByGenreAsync(Genre genre);
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
    Task AddAsync(Movie movie);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(int id);
    Task<MoviePaginateResponse> GetMoviesAsync(string search, string orderBy, string? sort, int page, int pageSize);
}