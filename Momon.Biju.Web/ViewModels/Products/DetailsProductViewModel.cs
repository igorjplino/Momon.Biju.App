namespace Momon.Biju.Web.ViewModels.Products;

public class DetailsProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryName { get; set; }
    public List<string> SubCategories { get; set; }
}