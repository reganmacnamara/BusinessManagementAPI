using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Data
{
    public class SQLContext(DbContextOptions<SQLContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Receipt> Receipts { get; set; } = null!;
        public DbSet<ReceiptItem> ReceiptItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SQLContext).Assembly);
        }

        public IQueryable<T> GetEntities<T>() where T : class
            => Set<T>();

    }
}
