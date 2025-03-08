using Microsoft.EntityFrameworkCore;
using TicketService.Application.DTOs.Requests;
using TicketService.Application.Interfaces.Repositories;
using TicketService.Domain.Entities;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Infrastructure.Presistence.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _dbContext;

        public TicketRepository(TicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            return await _dbContext.Tickets
                .Where(t => t.Id == ticketId)
                .FirstOrDefaultAsync();
        }

        public async Task<Ticket?> GetTicketByIdWithSeatsAsync(int ticketId, int? userId)
        {
            var ticket = await _dbContext.Tickets
                .Include(t => t.Seat)
                .FirstOrDefaultAsync(t => t.Id == ticketId && (userId == null || t.UserId == userId));

            return ticket;
        }


        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _dbContext.Tickets
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByTicketIdAsync(int TicketId)
        {
            return await _dbContext.Tickets
                .Where(t => t.ScheduleId == TicketId)
                .ToListAsync();
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _dbContext.Tickets.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket != null)
            {
                _dbContext.Tickets.Remove(ticket);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> TicketExistsAsync(int ticketId)
        {
            return await _dbContext.Tickets
                .AnyAsync(t => t.Id == ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(TicketQueryParams queryParams, int? userId)
        {
            // Build your query based on the queryParams
            var query = _dbContext.Tickets.AsQueryable();

            // Filter by status if provided
            if (queryParams.Status != null && queryParams.Status.Count > 0)
            {
                query = query.Where(ticket => queryParams.Status.Contains(ticket.Status.ToString()));
            }

            if (userId != null)
            {
                query = query.Where(ticket => ticket.UserId == userId);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(queryParams.OrderBy))
            {
                query = queryParams.Sort == "asc"
                    ? query.OrderBy(ticket => EF.Property<object>(ticket, queryParams.OrderBy))
                    : query.OrderByDescending(ticket => EF.Property<object>(ticket, queryParams.OrderBy));
            }

            // Apply pagination
            var tickets = await query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return tickets;
        }

        public async Task<Ticket?> GetTicketBySeatAndScheduleAsync(int seatId, int scheduleId)
        {
            return await _dbContext.Tickets
                .FirstOrDefaultAsync(t => t.SeatId == seatId && t.ScheduleId == scheduleId);
        }
    }
}