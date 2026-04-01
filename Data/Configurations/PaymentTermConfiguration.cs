using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations
{

    public class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm>
    {
        public void Configure(EntityTypeBuilder<PaymentTerm> builder)
        {
            builder.HasKey(c => c.PaymentTermID);

            builder.Property(c => c.CompanyID)
                .IsRequired();

            builder.Property(c => c.PaymentTermName)
                .IsRequired();

            builder.Property(c => c.Days)
                .IsRequired();

            builder.Property(c => c.Months)
                .IsRequired();

            builder.Property(c => c.EndOfMonth)
                .IsRequired();

            builder.Property(c => c.OffsetFirst)
                .IsRequired();

            builder.HasOne(c => c.Company)
                .WithMany(c => c.PaymentTerms)
                .HasForeignKey(c => c.CompanyID);
        }
    }

}
