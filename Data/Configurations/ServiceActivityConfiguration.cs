using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations;

public class ServiceActivityConfiguration : IEntityTypeConfiguration<ServiceActivity>
{
    public void Configure(EntityTypeBuilder<ServiceActivity> builder)
    {
        builder.HasKey(sa => sa.ServiceActivityID);

        builder.Property(sa => sa.ServiceID)
            .IsRequired();

        builder.Property(sa => sa.ActivityName)
            .IsRequired();

        builder.Property(sa => sa.Description)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(sa => sa.SortOrder)
            .HasDefaultValue(0);

        builder.HasOne(sa => sa.Service)
            .WithMany(s => s.ServiceActivities)
            .HasForeignKey(sa => sa.ServiceID);
    }
}
