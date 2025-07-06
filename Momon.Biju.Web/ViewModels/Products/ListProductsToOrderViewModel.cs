using Momon.Biju.Web.Dtos;
using Momon.Biju.Web.Dtos.Products;
using X.PagedList;

namespace Momon.Biju.Web.ViewModels.Products;

public class ListProductsToOrderViewModel
{
    public FilterProductsInListDto Filter { get; set; }
    public IPagedList<ProductInListDto> Products { get; set; }
}