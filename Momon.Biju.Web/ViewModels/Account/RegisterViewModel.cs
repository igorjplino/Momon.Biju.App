using System.ComponentModel.DataAnnotations;

namespace Momon.Biju.Web.Models.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}