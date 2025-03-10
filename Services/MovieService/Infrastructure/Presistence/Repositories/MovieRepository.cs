﻿using Microsoft.EntityFrameworkCore;
using MovieService.Application.DTOs.Responses;
using MovieService.Application.Interfaces.Repositories;
using MovieService.Domain.Entities;
using MovieService.Application.Interfaces.Services;
using MovieService.Domain.Enums;

namespace MovieService.Infrastructure.Persistence.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _context;
    private readonly ISerilog<Movie> _logger;

    public MovieRepository(MovieDbContext context, ISerilog<Movie> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Movies.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving movie with ID {id}");
            throw;
        }
    }

    public async Task<Movie?> GetByTitleAsync(string title)
    {
        try
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Title == title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving movie with Title {title}");
            throw;
        }
    }

    public async Task<List<Movie?>> GetByGenreAsync(Genre genre)
    {
        try
        {
            return await _context.Movies.Where(m => m.Genre == genre).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving movie with Genre {genre}");
            throw;
        }
    }


    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        try
        {
            return await _context.Movies.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all movies");
            throw;
        }
    }

    public async Task<IEnumerable<Movie>> SearchAsync(string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm)) return await GetAllAsync();

            return await _context.Movies
                .Where(m => m.Title.Contains(searchTerm) || m.Description.Contains(searchTerm))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching movies with term '{searchTerm}'");
            throw;
        }
    }

    public async Task AddAsync(Movie movie)
    {
        try
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding a new movie");
            throw;
        }
    }

    public async Task UpdateAsync(Movie movie)
    {
        try
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating movie with ID {movie.Id}");
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var movie = await GetByIdAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"Error deleting movie with ID {id}");
            throw;
        }
    }

    public async Task<MoviePaginateResponse> GetMoviesAsync(string search, string orderBy, string? sort, int page,
        int pageSize)
    {
        try
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.Title.Contains(search) || m.Description.Contains(search));

            var totalRecords = await query.CountAsync();

            var validColumns = new List<string> { "Title", "Genre", "DurationInMinutes", "CreatedAt", "UpdatedAt" };
            if (!string.IsNullOrEmpty(orderBy) && validColumns.Contains(orderBy))
                query = sort?.ToLower() == "desc"
                    ? query.OrderByDescending(m => EF.Property<object>(m, orderBy))
                    : query.OrderBy(m => EF.Property<object>(m, orderBy));
            else
                query = query.OrderBy(m => m.Title);

            var movies = await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new MoviePaginateResponse()
            {
                Movies = movies,
                Metadata = new Metadata(page, pageSize, totalRecords)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving movies with search, order, and pagination parameters");
            throw;
        }
    }
}