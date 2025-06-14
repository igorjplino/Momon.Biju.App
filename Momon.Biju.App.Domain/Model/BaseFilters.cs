namespace Momon.Biju.App.Domain.Model;

public record BaseFilters
{
    private const int MaxPageSize = 50;
    private const int PageNumberDefault = 1;
    private const int PageSizeDefault = 10;
    
    protected BaseFilters()
    {
        PageNumber = PageNumberDefault;
        PageSize = PageSizeDefault;
    }
    
    protected BaseFilters(
        int? pageNumber,
        int? pageSize)
    {
        PageNumber = pageNumber is null or <= 0 ? PageNumberDefault : pageNumber.Value;
        PageSize = pageSize switch
        {
            null => PageSizeDefault,
            > MaxPageSize => MaxPageSize,
            <= 0 => PageSizeDefault,
            _ => pageSize.Value
        };
    }

    public int PageNumber { get; }
    public int PageSize { get; }
}