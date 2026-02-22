using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(c => c.InvoiceID);

            builder.Property(c => c.InvoiceRef)
                .IsRequired();

            builder.Property(c => c.InvoiceDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(c => c.DueDate)
                .IsRequired();

            builder.Property(c => c.ClientID)
                .IsRequired();

            builder.Property(c => c.Outstanding)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(c => c.GrossValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.TaxValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.NetValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.OffsetValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(c => c.Client)
                .WithMany(c => c.Invoices)
                .HasForeignKey(c => c.ClientID);

            builder.HasMany(c => c.InvoiceItems)
                .WithOne(t => t.Invoice)
                .HasForeignKey(c => c.InvoiceID);
        }
    }
}
