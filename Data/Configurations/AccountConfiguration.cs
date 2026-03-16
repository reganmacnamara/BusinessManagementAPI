using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementAPI.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(c => c.AccountID);

            builder.Property(c => c.Email)
                .IsRequired();

            builder.Property(c => c.Password)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(c => c.LastLoginDate);
        }
    }
}
