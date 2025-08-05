using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.Web.ViewComponents;

public class CategorySelectViewComponent : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;

    public CategorySelectViewComponent(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(
        Guid? selectedCategoryId = null,
        string fieldName = "CategoryId",
        string label = "Categoria")
    {
        var categories = await _categoryRepository.GetAllAsync();
        
        var categoriesToSelect = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name,
            Selected = (c.Id == selectedCategoryId)
        }).ToList();
        
        ViewData["FieldName"] = fieldName;
        ViewData["Label"] = label;
        
        return View(categoriesToSelect);
    }
}