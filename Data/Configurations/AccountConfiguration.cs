using MacsBusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacsBusinessManagementAPI.Data.Configurations
{

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.AccountID);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.Property(a => a.Password)
                .IsRequired();

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }

}
