using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientID);

        builder.Property(c => c.CompanyID)
            .IsRequired();

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

        builder.HasOne(c => c.Company)
                .WithMany(c => c.Clients)
                .HasForeignKey(c => c.CompanyID);

        builder.HasMany(c => c.Invoices)
                .WithOne(c => c.Client)
                .HasForeignKey(c => c.ClientID);

        builder.HasMany(c => c.Receipts)
                .WithOne(c => c.Client)
                .HasForeignKey(c => c.ClientID);

        builder.HasOne(c => c.PaymentTerm)
                .WithOne();
    }
}
