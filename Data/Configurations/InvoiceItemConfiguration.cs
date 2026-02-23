using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.HasKey(c => c.InvoiceItemID);

        builder.Property(c => c.InvoiceID)
            .IsRequired();

        builder.Property(c => c.ProductID)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.Quantity)
            .HasDefaultValue(0);

        builder.Property(c => c.PricePerUnit)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(c => c.GrossValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(c => c.TaxValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(c => c.NetValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.HasOne(c => c.Invoice)
                .WithMany()
                .HasForeignKey(c => c.InvoiceID);

        builder.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductID);
    }
}
