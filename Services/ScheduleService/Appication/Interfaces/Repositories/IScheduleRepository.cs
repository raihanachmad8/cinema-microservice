using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

namespace ScheduleService.Application.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(int id);
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<IEnumerable<Schedule>> GetByShowTimeAsync(DateTime time, int studioId, int Duration);
    Task AddAsync(Schedule schedule);
    Task UpdateAsync(Schedule schedule);
    Task DeleteAsync(int id);
    
    Task DeleteSchedulesByMovieIdAsync(int movieId);
    Task DeleteSchedulesByStudioIdAsync(int scheduleId);

    Task<SchedulePaginateResponse> GetSchedulesAsync(int? movieId, int? studioId, string orderBy, string? sort, int page, int pageSize);
}