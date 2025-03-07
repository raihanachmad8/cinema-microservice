using TicketService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Requests;

namespace TicketService.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task<Ticket?> GetTicketByIdWithSeats(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
        Task<IEnumerable<Ticket>> GetTicketsByTicketIdAsync(int TicketId);
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
        Task<bool> TicketExistsAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsAsync(TicketQueryParams queryParams, int? userId = null);
        
        Task<Ticket?> GetTicketBySeatAndScheduleAsync(int seatId, int scheduleId);
    }
}