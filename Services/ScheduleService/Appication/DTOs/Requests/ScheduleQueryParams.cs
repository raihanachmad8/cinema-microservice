namespace ScheduleService.Application.DTOs.Requests;

public record ScheduleQueryParams
{
    public string? Search { get; set; }
    public string? OrderBy { get; set; }
    public string? Sort { get; set; } = "asc";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}