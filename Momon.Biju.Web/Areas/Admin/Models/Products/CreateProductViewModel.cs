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
    
    [Display(Name = "Número de Referência")]
    public string ReferenceNumber { get; set; }

    [Display(Name = "Categoria")]
    public Guid CategoryId { get; set; }
    
    [Display(Name = "Ativo")]
    public bool Active { get; set; }

    [Display(Name = "Imagem do produto")]
    public IFormFile? ImagePath { get; set; }
    
    [Display(Name = "Sub Categoria")]
    public List<Guid> SubCategories { get; set; }
    
    public IEnumerable<SelectListItem> CategoriesToSelect { get; set; }
    public IEnumerable<SelectListItem> SubCategoriesToSelect { get; set; }
}