namespace Momon.Biju.Web.Models;

public class ListProductViewModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }

    public ProductFilter Filters { get; set; }
    public List<Product> Products { get; set; }
}