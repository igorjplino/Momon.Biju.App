using System.ComponentModel.DataAnnotations;
using Momon.Biju.App.Domain.Entities.Identity;

namespace Momon.Biju.Web.Models.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}