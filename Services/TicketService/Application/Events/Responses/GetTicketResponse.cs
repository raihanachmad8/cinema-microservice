
namespace TicketService.Application.Events.Responses;

public class GetTicketResponse
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public string Status { get; set; }
    public DateTime? ReservedAt { get; set; }
}