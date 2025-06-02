namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class FilterProductsInListDto
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }

    public List<CategoryViewModel> Categories { get; set; } = [];
}