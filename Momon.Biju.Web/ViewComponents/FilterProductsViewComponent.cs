using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.CookieManagers;
using Momon.Biju.Web.Dtos.Products;

namespace Momon.Biju.Web.ViewComponents;

public class FilterProductsViewComponent : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly FilterProductsCookieManager _filterProductsCookieManager;

    public FilterProductsViewComponent(
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository,
        FilterProductsCookieManager filterProductsCookieManager)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _filterProductsCookieManager = filterProductsCookieManager;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        FilterProductsInListDto? filter = _filterProductsCookieManager.GetFilters();

        filter ??= new FilterProductsInListDto();
        
        if (filter.Categories is null || !filter.Categories.Any())
        {
            var categories = await _categoryRepository.GetAllAsync();
        
            filter.Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }
        
        if (filter.SubCategories is null || !filter.SubCategories.Any())
        {
            var subcategories = await _subCategoryRepository.GetAllAsync();
        
            filter.SubCategories = subcategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

        _filterProductsCookieManager.SaveFilter(filter);

        return View(filter);
    }
}