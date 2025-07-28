using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Areas.Cart.Models;

namespace Momon.Biju.Web.Areas.Cart.ViewComponents;

public class FinishPurchaseViewComponent : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public FinishPurchaseViewComponent(
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
    }

    public IViewComponentResult Invoke(DetailsToPurchase? model = null)
    {
        model ??= new DetailsToPurchase();
        return View(model);
    }
}