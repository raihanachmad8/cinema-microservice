using System.Text.Json.Serialization;

namespace ScheduleService.Application.DTOs.Responses;

public record ScheduleResponse
{
    public int Id { get; set; } 
    public Guid MovieId { get; set; }
    public Guid StudioId { get; set; }
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public decimal TicketPrice { get; set; } 
}