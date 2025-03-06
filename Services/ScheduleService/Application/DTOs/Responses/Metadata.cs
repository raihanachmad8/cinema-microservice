namespace ScheduleService.Application.DTOs.Responses;

public record Metadata
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; }
    public int TotalRecords { get; set; }

    public Metadata(int page, int pageSize, int totalRecords)
    {
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    public int GetFirstPageNumber()
    {
        return TotalRecords > 0 ? 1 : 0;
    }

    public int GetLastPageNumber()
    {
        return TotalPages > 0 ? TotalPages : 0;
    }
}