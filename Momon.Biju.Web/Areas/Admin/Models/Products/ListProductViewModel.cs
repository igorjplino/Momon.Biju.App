using System.Collections;

namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class ListProductViewModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }

    public FilterProductsInListDto Filter { get; set; }
    public IEnumerable<ProductInListDto> Products { get; set; }
}