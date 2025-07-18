using Momon.Biju.Web.Dtos;
using Momon.Biju.Web.Dtos.Products;
using X.PagedList;

namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class ListProductViewModel
{
    public FilterProductsInListDto Filter { get; set; }
    public IPagedList<ProductInListDto> Products { get; set; }
}