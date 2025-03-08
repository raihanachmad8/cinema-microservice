using System.Text.Json.Serialization;
using ScheduleService.Application.Events.Responses;

namespace ScheduleService.Application.DTOs.Responses;

public record ScheduleResponse
{
    public int Id { get; set; } 
    public int  MovieId { get; set; }
    public int StudioId { get; set; }
    public GetMovieResponse? Movie { get; set; } 
    public GetStudioResponse? Studio { get; set; }
    public decimal TicketPrice { get; set; } 
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
}