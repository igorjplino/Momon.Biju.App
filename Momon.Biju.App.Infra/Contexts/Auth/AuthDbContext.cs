using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Momon.Biju.App.Domain.Entities.Identity;

namespace Momon.Biju.App.Infra.Contexts.Auth;

public class AuthDbContext : IdentityDbContext<AppUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) 
        : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Move Identity tables to 'auth' schema
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            if (entity.ClrType.Namespace is not null && 
                entity.ClrType.Namespace.Contains("Identity"))
            {
                builder.Entity(entity.ClrType).ToTable(entity.GetTableName()!, "auth");
            }
        }
    }
}