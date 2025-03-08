using System.Text.Json.Serialization;

namespace TransactionService.Application.DTOs.Responses
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public string Status { get; set; }
        public DateTime? ReservedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SeatResponse? Seat { get; set; } // Ensure this property exists
    }
}