namespace Momon.Biju.App.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ReferenceNumber { get; set; }
    public bool Active { get; set; }
    public string ImagePath { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<ProductSubCategory> SubCategories { get; set; }
}