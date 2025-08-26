using System.ComponentModel.DataAnnotations;

namespace Momon.Biju.Web.Areas.Cart.Models;

public class DetailsToPurchase
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter entre {2} e {1} caracteres.")]
    [Display(Name = "Nome")]
    public string? Name { get; init; }
    
    [MaxLength(100, ErrorMessage = "Informações adicionais deve ter no máximo {1} caracteres")]
    [Display(Name = "Informações adicionais")]
    public string? AdditionalInformation { get; init; }

    public bool CanPurchase { get; init; }
}