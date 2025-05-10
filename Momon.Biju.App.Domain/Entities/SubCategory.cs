namespace Momon.Biju.App.Domain.Entities;

public class SubCategory : BaseEntity
{
    public string Name { get; set; }
    public ICollection<ProductSubCategory> Products { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}