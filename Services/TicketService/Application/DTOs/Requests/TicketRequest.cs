namespace TicketService.Application.DTOs.Requests
{
    public record TicketRequest
    {
        public int ScheduleId { get; set; } 
        public int SeatId { get; set; }
    }
}