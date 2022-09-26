using Microsoft.EntityFrameworkCore;
using ModelsDB;

namespace Services
{
    public class BankContext : DbContext
    {
        public DbSet<ClientDB>? Clients { get; set; }
        public DbSet<EmployeeDB>? Employees { get; set; }
        public DbSet<AccountDB>? Accounts { get; set; }
        public DbSet<CurrencyDB>? Currencys { get; set; }
   
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = BankDB; Username = postgres; Password = uG7L7%wc");
        }
    }
}
