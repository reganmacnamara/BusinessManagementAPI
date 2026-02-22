using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.HasKey(c => c.ReceiptID);

        builder.Property(c => c.ReceiptRef)
            .IsRequired();

        builder.Property(c => c.ReceiptDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(c => c.ClientID)
            .IsRequired();

        builder.Property(c => c.Outstanding)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(c => c.GrossValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.TaxValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.NetValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.OffsetValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(c => c.Client)
            .WithMany(c => c.Receipts)
            .HasForeignKey(c => c.ClientID);

        builder.HasMany(c => c.ReceiptItems)
            .WithOne(t => t.Receipt)
            .HasForeignKey(c => c.ReceiptID);
    }
}
