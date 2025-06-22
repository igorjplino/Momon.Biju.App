using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.App.Infra.Contexts.Configs;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(x => x.Active)
            .IsRequired();

        builder.Property(x => x.ImagePath)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.ReferenceNumber)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(x => x.ReferenceNumber)
            .IsUnique();
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}