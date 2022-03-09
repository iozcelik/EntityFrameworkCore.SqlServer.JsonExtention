using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test;
public class TestContext : DbContext
{
    public TestContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JsonExtentionTest;Integrated Security=True",
            providerOptions => providerOptions.CommandTimeout(60));

        optionsBuilder.UseJsonFunctions();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().Property(p => p.CountryDetail).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.ExtraInformation).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.UtcTimeZones).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.OfficialLanguages).HasJsonConversion();
    }
}

