using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.Data
{
    public class SQLContext(DbContextOptions<SQLContext> options, ITenantProvider tenantProvider) : DbContext(options)
    {
        public long AccountID = tenantProvider.AccountID;
        public long CompanyID = tenantProvider.CompanyID;

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public DbSet<PaymentTerm> PaymentTerms { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Receipt> Receipts { get; set; } = null!;
        public DbSet<ReceiptItem> ReceiptItems { get; set; } = null!;
        public DbSet<ReminderLog> ReminderLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SQLContext).Assembly);

            modelBuilder.Entity<Account>().HasQueryFilter(e => e.CompanyID == CompanyID);
            modelBuilder.Entity<Client>().HasQueryFilter(e => e.CompanyID == CompanyID);
            modelBuilder.Entity<Invoice>().HasQueryFilter(e => e.CompanyID == CompanyID);
            modelBuilder.Entity<Product>().HasQueryFilter(e => e.CompanyID == CompanyID);
            modelBuilder.Entity<PaymentTerm>().HasQueryFilter(e => e.CompanyID == CompanyID);
            modelBuilder.Entity<Receipt>().HasQueryFilter(e => e.CompanyID == CompanyID);
        }

        public IQueryable<T> GetEntities<T>() where T : class
            => Set<T>().AsQueryable();

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            if (CompanyID != 0)
                foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
                {
                    var _CompanyIDProperty = entry.Properties
                        .FirstOrDefault(p => p.Metadata.Name == "CompanyID");

                    if (_CompanyIDProperty is null)
                        continue;

                    var _Value = _CompanyIDProperty.CurrentValue;
                    if (_Value is null || _Value is 0L)
                        _CompanyIDProperty.CurrentValue = CompanyID;
                }

            return await base.SaveChangesAsync(ct);
        }
    }
}
