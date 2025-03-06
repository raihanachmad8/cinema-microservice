
namespace ScheduleService.Application.DTOs.Requests
{
    public class ScheduleRequest
    {
        public int MovieId { get; set; }
        public int StudioId { get; set; }
        public DateTime StartDatetime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}