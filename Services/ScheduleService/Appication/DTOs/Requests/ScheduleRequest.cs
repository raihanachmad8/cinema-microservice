
namespace ScheduleService.Application.DTOs.Requests
{
    public class ScheduleRequest
    {
        public Guid MovieId { get; set; }
        public Guid StudioId { get; set; }
        public DateTime StartDatetime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}