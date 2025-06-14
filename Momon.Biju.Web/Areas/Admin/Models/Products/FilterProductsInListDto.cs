using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class FilterProductsInListDto
{
    [Display(Name = "Nome")]
    public string Name { get; set; }
    [Display(Name = "Categoria")]
    public Guid? SelectedCategoryId { get; set; }
    [Display(Name = "Subcategoria")]
    public Guid? SelectedSubCategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> SubCategories { get; set; }
    
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}