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
        bool? active = null,
        Guid? categoryId = null,
        Guid? subCategoryId = null)
        : base(pageNumber, pageSize)
    {
        Name = name;
        Active = active;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }

    public string? Name { get; }
    public bool? Active { get; }
    public Guid? CategoryId { get; }
    public Guid? SubCategoryId { get; }
}