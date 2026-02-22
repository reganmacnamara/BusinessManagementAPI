using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductID);

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

        builder.HasMany(p => p.InvoiceItems)
            .WithOne(ii => ii.Product)
            .HasForeignKey(ii => ii.ProductID);
    }
}
