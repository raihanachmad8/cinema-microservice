using StudioService.Application.DTOs.Responses;
using StudioService.Domain.Entities;

namespace StudioService.Application.Interfaces.Repositories
{
    public interface IStudioRepository
    {
        Task<Studio?> GetByIdAsync(Guid id);
        Task<Studio?> GetByNameAsync(string name);
        Task<IEnumerable<Studio>> GetAllAsync();
        Task<IEnumerable<Studio>> SearchAsync(string searchTerm);
        Task AddAsync(Studio studio);
        Task UpdateAsync(Studio studio);
        Task DeleteAsync(Guid id);
        Task<StudioPaginateResponse> GetStudiosAsync(string search, string orderBy, string? sort, int page, int pageSize);
    }
}