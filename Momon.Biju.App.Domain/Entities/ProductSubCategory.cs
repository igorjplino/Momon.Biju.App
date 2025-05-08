namespace Momon.Biju.App.Domain.Entities;

public class ProductSubCategory
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
}