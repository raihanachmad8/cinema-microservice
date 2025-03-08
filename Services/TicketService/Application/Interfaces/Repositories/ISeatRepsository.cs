using TicketService.Domain.Entities;

namespace TicketService.Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<Seat?> GetSeatByIdAsync(int seatId);
        Task<IEnumerable<Seat>> GetSeatsByStudioIdAsync(int studioId);
        Task<IEnumerable<Seat>> GetAvailableSeatsByStudioIdAsync(int studioId);
        Task AddSeatAsync(Seat seat);
        Task UpdateSeatAsync(Seat seat);
        Task DeleteSeatAsync(int seatId);
        Task<bool> SeatExistsAsync(int seatId);
    }
}