namespace Momon.Biju.App.Domain.Model;

public record ProductFilters : BaseFilters
{
    public ProductFilters()
    {
    }

    public ProductFilters(
        int? pageNumber = 1,
        int? pageSize = 10,
        string? name = null,
        Guid? categoryId = null,
        Guid? subCategoryId = null)
        : base(pageNumber, pageSize)
    {
        Name = name;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }

    public string? Name { get; }
    public Guid? CategoryId { get; }
    public Guid? SubCategoryId { get; }
}