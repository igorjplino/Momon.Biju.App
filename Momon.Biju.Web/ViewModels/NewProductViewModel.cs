using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Models;

public class NewProductViewModel
{
    public string Name { get; set; }
    public string Price { get; set; }

    public Guid SelectedCategoryId { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    
    public List<Guid> SelectedSubCategoriesId { get; set; }
    public IEnumerable<SelectListItem> SubCategories { get; set; }
}