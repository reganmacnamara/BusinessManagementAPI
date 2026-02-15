using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class TransactionAllocationConfiguration : IEntityTypeConfiguration<TransactionAllocation>
{
    public void Configure(EntityTypeBuilder<TransactionAllocation> builder)
    {
        builder.HasKey(ta => ta.TransactionAllocationID);

        builder.Property(ta => ta.AllocationValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(ta => ta.AllocationDate)
            .IsRequired();

        // Configure relationships to Transaction as Allocating and Recieving
        builder.HasOne(ta => ta.AllocatingTransaction)
            .WithMany()
            .HasForeignKey(ta => ta.AllocatingID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ta => ta.RecievingTransaction)
            .WithMany()
            .HasForeignKey(ta => ta.RecievingID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
