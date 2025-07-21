using Momon.Biju.Web.Dtos;
using Momon.Biju.Web.Dtos.Products;
using X.PagedList;

namespace Momon.Biju.Web.ViewModels.Products;

public class ListProductsToOrderViewModel
{
    public IPagedList<ProductInListDto> Products { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}