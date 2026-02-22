using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class ReceiptItemConfiguration : IEntityTypeConfiguration<ReceiptItem>
{
    public void Configure(EntityTypeBuilder<ReceiptItem> builder)
    {
        builder.HasKey(c => c.ReceiptItemID);

        builder.Property(c => c.ReceiptID)
            .IsRequired();

        builder.Property(c => c.InvoiceID)
            .IsRequired();

        builder.Property(c => c.GrossValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(c => c.TaxValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(c => c.NetValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);
    }
}
