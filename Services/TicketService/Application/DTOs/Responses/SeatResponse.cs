using TicketService.Domain.Entities;

namespace TicketService.Application.DTOs.Responses
{
    public record SeatResponse
    {
        public int Id { get; set; }
        public int StudioId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public DateTime? ReservedAt { get; set; }
        public DateTime? OccupiedAt { get; set; }
    }
}
