using Microsoft.EntityFrameworkCore;
using ModelsDb;

namespace Services
{
    public class BankContext : DbContext
    {
        public DbSet<ClientDb>? Clients { get; set; }  
        public DbSet<AccountDb>? Accounts { get; set; }
        public DbSet<CurrencyDb>? Currencys { get; set; }
        public DbSet<EmployeeDb>? Employees { get; set; }
   
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = BankDB; Username = postgres; Password = uG7L7%wc");
        }
    }
}
