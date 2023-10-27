namespace WebApi.Core;

public class PaginationRequest
{
    public int CurrentPage { get; set; } = 1;
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public bool Direction { get; set; } = true;
    public int PageSize { get; set; } = 10;

    public PaginationResponse ToResponse()
    {
        return new PaginationResponse
        {
            PageSize = PageSize,
            CurrentPage = CurrentPage
        };
    }
}

public class PaginationResponse
{
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
}