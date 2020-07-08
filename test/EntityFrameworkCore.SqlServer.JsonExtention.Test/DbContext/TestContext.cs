using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class TestContext : DbContext {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JsonExtentionTest;Integrated Security=True",
                providerOptions => providerOptions.CommandTimeout(60));

            optionsBuilder.UseJsonFunctions();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>().Property(p => p.Company).HasJsonConversion();
            modelBuilder.Entity<Customer>().Property(p => p.ContactDetail).HasJsonConversion();
            modelBuilder.Entity<Customer>().Property(p => p.LuckyNumbers).HasJsonConversion();
            modelBuilder.Entity<Customer>().Property(p => p.MenuItems).HasJsonConversion();
        }
    }

}
