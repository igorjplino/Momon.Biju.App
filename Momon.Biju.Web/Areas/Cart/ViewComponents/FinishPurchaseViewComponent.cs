using Microsoft.AspNetCore.Mvc;
using Momon.Biju.Web.Models;

namespace Momon.Biju.Web.Areas.Cart.ViewComponents;

public class FinishPurchaseViewComponent: ViewComponent
{
    public IViewComponentResult Invoke(DetailsToPurchase? model = null)
    {
        model ??= new DetailsToPurchase();
        return View(model);
    }
}