namespace ScheduleService.Application.DTOs.Responses;

public record ScheduleResponse
{
    public int Id { get; set; } 
    public MovieResponse Movie { get; set; } 
    public StudioResponse Studio { get; set; } 
    public DateTime ShowTime { get; set; } 
    public decimal TicketPrice { get; set; }
}