using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Dtos.Products;

public class FilterProductsInListDto
{
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Display(Name = "Ativo")] 
    public bool? Active { get; set; }
    
    [Display(Name = "Categoria")]
    public Guid? SelectedCategoryId { get; set; }
    
    [Display(Name = "Subcategoria")]
    public Guid? SelectedSubCategoryId { get; set; }

    public IEnumerable<SelectListItem>? Categories { get; set; }
    public IEnumerable<SelectListItem>? SubCategories { get; set; }
} 