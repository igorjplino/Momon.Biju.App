using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Momon.Biju.Web.Areas.Admin.Models.Products;

public class EditProductViewModel
{
    public Guid Id { get; set; }
    [Display(Name = "Nome")]
    public string Name { get; set; }
    
    [Display(Name = "Descrição")]
    public string Description { get; set; }
    
    [Display(Name = "Preço")]
    public decimal Price { get; set; }
    
    [Display(Name = "Número de Referência")]
    public string ReferenceNumber { get; set; }

    [Display(Name = "Categoria")]
    public Guid SelectedCategoryId { get; set; }

    [Display(Name = "Imagem do produto")] 
    public string CurrentProductImage { get; set; }
    public IFormFile? NewProductImage { get; set; }
    
    [Display(Name = "Subcategoria")]
    public IEnumerable<Guid> SelectedSubCategoriesId { get; set; }
    
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> SubCategories { get; set; }
}