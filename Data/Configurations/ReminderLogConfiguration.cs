using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations;

public class ReminderLogConfiguration : IEntityTypeConfiguration<ReminderLog>
{
    public void Configure(EntityTypeBuilder<ReminderLog> builder)
    {
        builder.HasKey(r => r.ReminderLogID);

        builder.Property(r => r.ClientID)
            .IsRequired();

        builder.Property(r => r.SentAt)
            .IsRequired();

        builder.Property(r => r.InvoiceCount)
            .IsRequired();

        builder.HasOne(r => r.Client)
            .WithMany()
            .HasForeignKey(r => r.ClientID);
    }
}
