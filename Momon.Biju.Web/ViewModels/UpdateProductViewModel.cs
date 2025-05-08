using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.Web.Models;

public class UpdateProductViewModel
{
    public UpdateProductViewModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price.ToString();
        SelectedCategoryId = product.CategoryId;
        SelectedSubCategoriesId = product.SubCategories.Select(x => x.SubCategoryId);
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    
    public Guid SelectedCategoryId { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    
    public IEnumerable<Guid> SelectedSubCategoriesId { get; set; }
    public IEnumerable<SelectListItem> SubCategories { get; set; }
}