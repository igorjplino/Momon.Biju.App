namespace Momon.Biju.App.Domain.Model;

public record BaseFilters
{
    private const int MaxPageSize = 50;
    
    protected BaseFilters()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    
    protected BaseFilters(
        int pageNumber,
        int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 50 ? MaxPageSize : pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
}