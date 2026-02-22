using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientID);

        builder.Property(c => c.ClientName)
            .IsRequired();

        builder.Property(c => c.ClientEmail)
            .IsRequired();

        builder.Property(c => c.ClientPhone)
            .IsRequired();

        builder.Property(c => c.ClientMobile)
            .IsRequired();

        builder.Property(c => c.AddressLine1)
            .IsRequired();

        builder.Property(c => c.AddressLine2)
            .IsRequired();

        builder.Property(c => c.PostCode)
            .IsRequired();

        builder.Property(c => c.State)
            .IsRequired();

        builder.Property(c => c.Country)
            .IsRequired();

        builder.HasMany(c => c.Invoices)
            .WithOne(t => t.Client)
            .HasForeignKey(t => t.ClientID);
    }
}
