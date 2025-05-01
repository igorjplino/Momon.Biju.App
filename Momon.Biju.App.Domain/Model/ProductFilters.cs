namespace Momon.Biju.App.Domain.Model;

public record ProductFilters : BaseFilters
{
    public ProductFilters()
    {
    }

    protected ProductFilters(
        int pageNumber = 1,
        int pageSize = 10,
        string? name = null)
        : base(pageNumber, pageSize)
    {
        Name = name;
    }

    public string? Name { get; set; }
}