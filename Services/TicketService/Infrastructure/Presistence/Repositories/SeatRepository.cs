using Microsoft.EntityFrameworkCore;
using TicketService.Application.Interfaces.Repositories;
using TicketService.Domain.Entities;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly TicketDbContext _dbContext;

        public SeatRepository(TicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Seat?> GetSeatByIdAsync(int seatId)
        {
            return await _dbContext.Seats
                .Where(s => s.Id == seatId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsByStudioIdAsync(int studioId)
        {
            return await _dbContext.Seats
                .Where(s => s.StudioId == studioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetAvailableSeatsByStudioIdAsync(int studioId)
        {
            return await _dbContext.Seats
                .Where(s => s.StudioId == studioId && s.IsAvailable)
                .ToListAsync();
        }

        public async Task AddSeatAsync(Seat seat)
        {
            await _dbContext.Seats.AddAsync(seat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSeatAsync(Seat seat)
        {
            _dbContext.Seats.Update(seat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSeatAsync(int seatId)
        {
            var seat = await GetSeatByIdAsync(seatId);
            if (seat != null)
            {
                _dbContext.Seats.Remove(seat);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> SeatExistsAsync(int seatId)
        {
            return await _dbContext.Seats
                .AnyAsync(s => s.Id == seatId);
        }
    }
}