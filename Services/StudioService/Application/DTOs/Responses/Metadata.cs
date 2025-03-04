namespace StudioService.Application.DTOs.Responses;

public record Metadata
{
    public int FirstPageNumber => TotalPages > 0 ? 1 : 0;
    public int LastPageNumber => TotalPages;
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public Metadata(int page, int pageSize, int totalRecords)
    {
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }
}