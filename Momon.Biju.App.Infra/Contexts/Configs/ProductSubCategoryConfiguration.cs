using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.App.Infra.Contexts.Configs;

public class ProductSubCategoryConfiguration : IEntityTypeConfiguration<ProductSubCategory>
{
    public void Configure(EntityTypeBuilder<ProductSubCategory> builder)
    {
        builder.ToTable("ProductSubCategories");
        
        builder.HasKey(x => new { x.ProductId, x.SubCategoryId });

        builder.HasOne(x => x.Product)
            .WithMany(x => x.SubCategories)
            .HasForeignKey(x => x.ProductId);
        
        builder.HasOne(x => x.SubCategory)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SubCategoryId);
    }
}