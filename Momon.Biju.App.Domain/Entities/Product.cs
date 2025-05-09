namespace Momon.Biju.App.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public IEnumerable<ProductSubCategory> SubCategories { get; set; }
}