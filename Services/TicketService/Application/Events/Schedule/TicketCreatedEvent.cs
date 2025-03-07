using TicketService.Application.DTOs.Responses;
using TicketService.Domain.Enums;

namespace TicketService.Appication.Events.User;

public class TicketCreatedEvent
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime? ReservedAt { get; set; }
    public SeatResponse? Seat { get; set; }
}