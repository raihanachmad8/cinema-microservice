namespace StudioService.Application.DTOs.Requests
{
    public class StudioQueryParams
    {
        public string? Search { get; set; }
        public List<string>? Columns { get; set; }
        public string? OrderBy { get; set; }
        public string? Sort { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}