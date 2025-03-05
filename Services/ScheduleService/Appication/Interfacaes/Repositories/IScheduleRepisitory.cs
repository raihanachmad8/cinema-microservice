using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

namespace ScheduleService.Appication.Interfacaes.Repositories;

public interface IScheduleRepisitory
{
    Task<Schedule?> GetByIdAsync(int id);
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<IEnumerable<Schedule>> GetSchedulesByFilmIdAsync(Guid filmId);
    Task<IEnumerable<Schedule>> GetSchedulesByStudioIdAsync(Guid studioId);
    Task AddAsync(Schedule schedule);
    Task UpdateAsync(Schedule schedule);
    Task DeleteAsync(int id);

    Task<SchedulePaginateResponse> GetSchedulesAsync(string search, string orderBy, string? sort, int page,
        int pageSize);
}