using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations
{

    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.CompanyID);

            builder.Property(c => c.CompanyName)
                .IsRequired();

            builder.Property(c => c.CompanyABN);

            builder.Property(c => c.Email)
                .IsRequired();

            builder.Property(c => c.Phone);

            builder.Property(c => c.AddressLine1)
                .HasMaxLength(100);

            builder.Property(c => c.AddressLine2)
                .HasMaxLength(100);

            builder.Property(c => c.State);

            builder.Property(c => c.PostCode);

            builder.Property(c => c.Country);

            builder.HasMany(c => c.Accounts)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Clients)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Invoices)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.PaymentTerms)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Products)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Receipts)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }

}
