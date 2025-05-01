using Microsoft.AspNetCore.Identity;

namespace Momon.Biju.App.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
}