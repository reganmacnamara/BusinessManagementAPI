using BusinessManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Data
{
    public class SQLContext(DbContextOptions<SQLContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionAllocation> TransactionAllocations { get; set; } = null!;
        public DbSet<TransactionItem> TransactionItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SQLContext).Assembly);
        }
    }
}
