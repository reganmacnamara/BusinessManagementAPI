using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.HasKey(ti => ti.TransactionItemID);

        builder.Property(ti => ti.Quantity)
            .IsRequired();

        builder.Property(ti => ti.PricePerItem)
            .HasColumnType("decimal(18,2)");

        builder.Property(ti => ti.LineGross)
            .HasColumnType("decimal(18,2)");

        builder.Property(ti => ti.LineTax)
            .HasColumnType("decimal(18,2)");

        builder.Property(ti => ti.LineNet)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(ti => ti.Product)
            .WithMany()
            .HasForeignKey(ti => ti.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ti => ti.Transaction)
            .WithMany()
            .HasForeignKey(ti => ti.TransactionID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
