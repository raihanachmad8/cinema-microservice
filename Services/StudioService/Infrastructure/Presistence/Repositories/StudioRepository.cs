using Microsoft.EntityFrameworkCore;
using StudioService.Application.DTOs.Responses;
using StudioService.Application.Interfaces.Repositories;
using StudioService.Domain.Entities;
using StudioService.Application.Interfaces.Services;

namespace StudioService.Infrastructure.Persistence.Repositories;

public class StudioRepository : IStudioRepository
{
    private readonly StudioDbContext _context;
    private readonly ILoggerService<Studio> _logger;

    public StudioRepository(StudioDbContext context, ILoggerService<Studio> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Studio?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Studios.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving studio with ID {id}", ex);
            throw;
        }
    }

    public async Task<Studio?> GetByNameAsync(string name)
    {
        try
        {
            return await _context.Studios.FirstOrDefaultAsync(s => s.Name == name);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving studio with Name {name}", ex);
            throw;
        }
    }

    public async Task<IEnumerable<Studio>> GetAllAsync()
    {
        try
        {
            return await _context.Studios.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error retrieving all studios", ex);
            throw;
        }
    }

    public async Task<IEnumerable<Studio>> SearchAsync(string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm)) return await GetAllAsync();

            return await _context.Studios
                .Where(s => s.Name.Contains(searchTerm) || s.AdditionalFacilities.Contains(searchTerm))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error searching studios with term '{searchTerm}'", ex);
            throw;
        }
    }

    public async Task AddAsync(Studio studio)
    {
        try
        {
            await _context.Studios.AddAsync(studio);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error adding a new studio", ex);
            throw;
        }
    }

    public async Task UpdateAsync(Studio studio)
    {
        try
        {
            _context.Studios.Update(studio);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating studio with ID {studio.Id}", ex);
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var studio = await GetByIdAsync(id);
            if (studio != null)
            {
                _context.Studios.Remove(studio);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting studio with ID {id}", ex);
            throw;
        }
    }

    public async Task<StudioPaginateResponse> GetStudiosAsync(string search,
        string orderBy, string? sort, int page, int pageSize)
    {
        try
        {
            var query = _context.Studios.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search) || s.AdditionalFacilities.Contains(search));

            var totalRecords = await query.CountAsync();

            var validColumns = new List<string> { "Name", "Capacity", "CreatedAt", "UpdatedAt" };
            if (!string.IsNullOrEmpty(orderBy) && validColumns.Contains(orderBy))
                query = sort?.ToLower() == "desc"
                    ? query.OrderByDescending(s => EF.Property<object>(s, orderBy))
                    : query.OrderBy(s => EF.Property<object>(s, orderBy));
            else
                query = query.OrderBy(s => s.Name);

            var studio = await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new StudioPaginateResponse()
            {
                Studios = studio,
                Metadata = new Metadata(page, pageSize, totalRecords)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error retrieving studios with search, order, and pagination parameters", ex);
            throw;
        }
    }
}