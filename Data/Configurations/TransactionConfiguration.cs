using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionID);

        builder.Property(t => t.TransactionRef)
            .IsRequired();

        builder.Property(t => t.TransactionType)
            .IsRequired();

        builder.Property(t => t.GrossValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.TaxValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.NetValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.OffsetValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.OffsetingValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.OutstandingValue)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(t => t.Client)
            .WithMany()
            .HasForeignKey(t => t.ClientID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
