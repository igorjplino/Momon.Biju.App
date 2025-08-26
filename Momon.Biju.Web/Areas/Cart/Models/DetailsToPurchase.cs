using System.ComponentModel.DataAnnotations;

namespace Momon.Biju.Web.Areas.Cart.Models;

public class DetailsToPurchase
{
    [Required]
    [MaxLength(50)]
    [Display(Name = "Nome")]
    public string Name { get; set; }
    [MaxLength(100)]
    [Display(Name = "Informações adicionais")]
    public string AdditionalInformation { get; set; }

    public bool CanPurchase { get; set; }
}