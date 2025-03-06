namespace ScheduleService.Application.DTOs.Responses;

public record MovieResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public string Description { get; set; } = string.Empty;
}