namespace Momon.Biju.App.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; }
}