using Microsoft.EntityFrameworkCore;
using ScheduleService.Application.DTOs.Responses;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Domain.Entities;
using ScheduleService.Application.Interfaces.Services;
using ScheduleService.Infrastructure.Persistence;

namespace ScheduleService.Infrastructure.Persistence.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ScheduleDbContext _context; // Ganti dengan konteks database Anda
        private readonly ISerilog<Schedule> _logger;

        public ScheduleRepository(ScheduleDbContext context, ISerilog<Schedule> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Schedules.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving schedule with ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync() => await _context.Schedules.ToListAsync();

        public async Task<IEnumerable<Schedule>> GetByShowTimeAsync(DateTime time, int studioId, int duration)
        {
            try
            {
                var startTime = time;
                var endTime = time.AddMinutes(duration);

                return await _context.Schedules
                    .Where(s => s.StudioId == studioId &&
                                s.StartDatetime < endTime &&
                                s.EndDatetime > startTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving schedules for studio {studioId} at {time}");
                throw;
            }
        }

        public async Task AddAsync(Schedule schedule)
        {
            try
            {
                await _context.Schedules.AddAsync(schedule);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new schedule");
                throw;
            }
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            try
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating schedule with ID {schedule.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var schedule = await GetByIdAsync(id);
                if (schedule != null)
                {
                    _context.Schedules.Remove(schedule);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting schedule with ID {id}");
                throw;
            }
        }

        public async Task DeleteSchedulesByMovieIdAsync(int movieId)
        {
            try
            {
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    "DELETE FROM Schedules WHERE MovieId = {0}", movieId);

                _logger.LogInformation($"{rowsAffected} schedules deleted for movie with ID {movieId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting schedules for movie with ID {movieId}");
                throw;
            }
        }

        public async Task DeleteSchedulesByStudioIdAsync(int studioId)
        {
            try
            {
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    "DELETE FROM Schedules WHERE StudioId = {0}", studioId);

                _logger.LogInformation($"{rowsAffected} schedules deleted for studio with ID {studioId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Error deleting schedules for studio with ID {studioId}");
                throw;
            }
        }


        public async Task<SchedulePaginateResponse> GetSchedulesAsync(int? movieId, int? studioId, string orderBy,
            string? sort, int page, int pageSize)
        {
            try
            {
                var query = _context.Schedules.AsQueryable();
                if (movieId.HasValue)
                {
                    query = query.Where(s => s.MovieId == movieId.Value);
                    Console.WriteLine("After filtering by MovieId: " + query.ToString());
                }

                if (studioId.HasValue)
                {
                    query = query.Where(s => s.StudioId == studioId.Value);
                    Console.WriteLine("After filtering by StudioId: " + query.ToString());
                }

                var totalRecords = await query.CountAsync();

                // Sorting logic
                var validColumns = new List<string>
                {
                    "ShowTime", "MovieId", "StudioId", "ShowTime", "TicketPrice", "CreatedAt", "UpdatedAt"
                };
                if (!string.IsNullOrEmpty(orderBy) && validColumns.Contains(orderBy))
                {
                    query = sort?.ToLower() == "desc"
                        ? query.OrderByDescending(s => EF.Property<object>(s, orderBy))
                        : query.OrderBy(s => EF.Property<object>(s, orderBy));
                }
                else
                {
                    query = query.OrderBy(s => s.StartDatetime);
                }

                var schedules = await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new SchedulePaginateResponse()
                {
                    Schedules = schedules,
                    Metadata = new Metadata(page, pageSize, totalRecords)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error retrieving schedules with pagination parameters");
                throw;
            }
        }
    }
}