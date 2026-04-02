using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations
{

    public class CompanySettingsConfiguration : IEntityTypeConfiguration<CompanySettings>
    {
        public void Configure(EntityTypeBuilder<CompanySettings> builder)
        {
            builder.HasKey(c => c.CompanySettingsID);

            builder.Property(c => c.InvoicePrefix);

            builder.Property(c => c.NextInvoiceNumber);

            builder.Property(c => c.AutoIncrementInvoice);

            builder.Property(c => c.ReceiptPrefix);

            builder.Property(c => c.NextReceiptNumber);

            builder.Property(c => c.AutoIncrementReceipt);
        }
    }

}
