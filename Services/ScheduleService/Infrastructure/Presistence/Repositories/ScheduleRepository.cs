﻿using Microsoft.EntityFrameworkCore;
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
        private readonly ILoggerService<Schedule> _logger;

        public ScheduleRepository(ScheduleDbContext context, ILoggerService<Schedule> logger)
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
                _logger.LogError($"Error retrieving schedule with ID {id}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync() => await _context.Schedules.ToListAsync();

        public async Task<IEnumerable<Schedule>> GetByShowTimeAsync(DateTime time, Guid studioId)
        {
            try
            {
                return await _context.Schedules
                    .Where(s => s.ShowTime.Date == time.Date && s.StudioId == studioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving schedules for studio {studioId} at {time}", ex);
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
                _logger.LogError("Error adding a new schedule", ex);
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
                _logger.LogError($"Error updating schedule with ID {schedule.Id}", ex);
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
                _logger.LogError($"Error deleting schedule with ID {id}", ex);
                throw;
            }
        }

        public async Task DeleteSchedulesByMovieIdAsync(Guid movieId)
        {
            try
            {
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    "DELETE FROM Schedules WHERE MovieId = {0}", movieId);

                _logger.LogInformation($"{rowsAffected} schedules deleted for movie with ID {movieId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting schedules for movie with ID {movieId}", ex);
                throw;
            }
        }

        public async Task DeleteSchedulesByStudioIdAsync(Guid studioId)
        {
            try
            {
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    "DELETE FROM Schedules WHERE StudioId = {0}", studioId);

                _logger.LogInformation($"{rowsAffected} schedules deleted for studio with ID {studioId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting schedules for studio with ID {studioId}", ex);
                throw;
            }
        }


        public async Task<SchedulePaginateResponse> GetSchedulesAsync(Guid? movieId, Guid? studioId, string orderBy,
            string? sort, int page, int pageSize)
        {
            try
            {
                var query = _context.Schedules.AsQueryable();
                Console.WriteLine(movieId != Guid.Empty);
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
                    query = query.OrderBy(s => s.ShowTime);
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
                _logger.LogError("Error retrieving schedules with pagination parameters", ex);
                throw;
            }
        }
    }
}