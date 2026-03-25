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

            builder.Property(c => c.PaymentTermName)
                .IsRequired();

            builder.Property(c => c.Value)
                .IsRequired();

            builder.Property(c => c.Unit)
                .IsRequired();

            builder.Property(c => c.IsEndOf)
                .IsRequired();

            builder.Property(c => c.IsStartingNext)
                .IsRequired();
        }
    }

}
