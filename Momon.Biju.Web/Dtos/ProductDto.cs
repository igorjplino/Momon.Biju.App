namespace Momon.Biju.Web.Models;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    
    public CategoryViewModel Category { get; set; }
}