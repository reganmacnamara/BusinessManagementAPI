using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductID);

        builder.Property(p => p.CompanyID)
            .IsRequired();

        builder.Property(p => p.ProductName)
            .IsRequired();

        builder.Property(p => p.ProductCode)
            .IsRequired();

        builder.Property(p => p.ProductDescription)
            .IsRequired(false);

        builder.Property(p => p.UnitCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.QuantityOnHand)
            .HasDefaultValue(0);

        builder.HasOne(c => c.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CompanyID);
    }
}
