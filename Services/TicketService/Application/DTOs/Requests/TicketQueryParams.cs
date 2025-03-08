namespace TicketService.Application.DTOs.Requests
{
    public record TicketQueryParams
    {
        public List<string>? Status { get; set; }
        public string? OrderBy { get; set; } = "UpdatedAt";
        public string? Sort { get; set; } = "desc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}