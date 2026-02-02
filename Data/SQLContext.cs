using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Data
{
    public class SQLContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionItem> TransactionItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: Remove this and use a real method to store this
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-OTB92DD;Initial Catalog=InvoiceAutomationLocal;User ID=sa;Password=lqslqs;TrustServerCertificate=True;");
        }
    }
}
