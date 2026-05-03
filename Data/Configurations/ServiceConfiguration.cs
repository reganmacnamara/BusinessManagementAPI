using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.ServiceID);

        builder.Property(s => s.CompanyID)
            .IsRequired();

        builder.Property(s => s.ServiceName)
            .IsRequired();

        builder.Property(s => s.ServiceDescription)
            .IsRequired(false);

        builder.Property(s => s.EstimatedDuration)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(s => s.DurationUnit)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.UnitCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(s => s.Company)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.CompanyID);

        builder.HasMany(s => s.ServiceActivities)
            .WithOne(sa => sa.Service)
            .HasForeignKey(sa => sa.ServiceID);
    }
}
