using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.ServiceID);

        builder.Property(s => s.ServiceName)
            .IsRequired(false);

        builder.Property(s => s.ServiceDescription)
            .IsRequired(false);

        builder.Property(s => s.ServiceGross)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.ServiceTax)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.ServiceNet)
            .HasColumnType("decimal(18,2)");
    }
}
