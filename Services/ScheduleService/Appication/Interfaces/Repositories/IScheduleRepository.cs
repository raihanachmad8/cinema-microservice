using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Domain.Entities;

namespace ScheduleService.Application.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(int id);
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<IEnumerable<Schedule>> GetByShowTimeAsync(DateTime time, Guid studioId);
    Task AddAsync(Schedule schedule);
    Task UpdateAsync(Schedule schedule);
    Task DeleteAsync(int id);
    
    Task DeleteSchedulesByMovieIdAsync(Guid movieId);
    Task DeleteSchedulesByStudioIdAsync(Guid scheduleId);

    Task<SchedulePaginateResponse> GetSchedulesAsync(Guid? movieId, Guid? studioId, string orderBy, string? sort, int page, int pageSize);
}