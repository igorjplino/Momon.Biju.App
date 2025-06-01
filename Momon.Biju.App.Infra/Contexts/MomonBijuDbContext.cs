using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.App.Infra.Contexts;

public class MomonBijuDbContext : DbContext
{
    public MomonBijuDbContext(DbContextOptions<MomonBijuDbContext> options) 
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<ProductSubCategory> ProductsSubCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}