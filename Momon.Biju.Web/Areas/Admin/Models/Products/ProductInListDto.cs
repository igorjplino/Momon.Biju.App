namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class ProductInListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    
    public CategoryViewModel Category { get; set; }
}