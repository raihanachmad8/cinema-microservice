namespace ScheduleService.Application.DTOs.Requests;

public record ScheduleQueryParams
{
    public string? MovieId { get; set; } = string.Empty;
    public string? StudioId { get; set; } = string.Empty;
    public string? OrderBy { get; set; }
    public string? Sort { get; set; } = "asc";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}