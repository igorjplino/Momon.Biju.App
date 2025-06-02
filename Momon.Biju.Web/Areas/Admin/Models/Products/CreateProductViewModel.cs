using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class CreateProductViewModel
{
    [Display(Name = "Nome")]
    public string Name { get; set; }
    
    [Display(Name = "Descrição")]
    public string Description { get; set; }
    
    [Display(Name = "Preço")]
    public string Price { get; set; }

    [Display(Name = "Categoria")]
    public Guid SelectedCategoryId { get; set; }

    [Display(Name = "Imagem do produto")]
    public IFormFile ProductImage { get; set; }
    
    [Display(Name = "Sub Categoria")]
    public List<Guid> SelectedSubCategoriesId { get; set; }
    
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> SubCategories { get; set; }
}