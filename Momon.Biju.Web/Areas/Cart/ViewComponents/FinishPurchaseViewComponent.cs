using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Models;

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

    public async Task<IViewComponentResult> InvokeAsync(DetailsToPurchase? model = null)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var subcategories = await _subCategoryRepository.GetAllAsync();
        
        model ??= new DetailsToPurchase();
        return View(model);
    }
}