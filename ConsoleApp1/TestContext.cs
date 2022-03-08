using EntityFrameworkCore.SqlServer.JsonExtention;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1;

public class TestContext : DbContext
{
    public TestContext(ILoggerFactory myLoggerFactory)
    {
        _loggerFactory=myLoggerFactory;
    }

    public DbSet<Customer> Customers { get; set; }
    public ILoggerFactory _loggerFactory { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JsonExtentionTest;Integrated Security=True",
            providerOptions => providerOptions.CommandTimeout(60));

        optionsBuilder.UseJsonFunctions();
        optionsBuilder.UseLoggerFactory(_loggerFactory); // this is optional I guess
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().Property(p => p.Company).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.ContactDetail).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.LuckyNumbers).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.MenuItems).HasJsonConversion();
        modelBuilder.Entity<Customer>().HasData(
            new Customer()
            {
                Id=1,
                Name = "John Doe",
                Company = new Company()
                {
                    Name = "John's Bank",
                    FoundDate = new DateTime(1978, 3, 5),
                    Branches = new List<Branch>() {
                        new Branch {
                            Name = "Istanbul Central",
                            City = "Istanbul",
                            Code = 34
                        },
                        new Branch {
                            Name = "HQ",
                            City = "Istanbul"
                        },
                        new Branch {
                            Name = "Ankara Capital",
                            City = "Ankara",
                            Code = 6
                        },
                        new Branch {
                            Name = "Ankara East",
                            City = "Ankara",
                            Code = 61
                        },
                    }
                },
                ContactDetail = new Dictionary<string, object>() {
                    { "Phone",123456789},
                    { "Email","info@johnsbank.com"}
                },
                LuckyNumbers = new List<int>() { 3, 8, 15 },
                MenuItems = new List<string>() { "MainMenu", "Transfers" }
            },
            new Customer()
            {
                Id=2,
                Name = "Jane Doe",
                Company = new Company()
                {
                    Name = "Jane Computer",
                    FoundDate = new DateTime(1995, 3, 5),
                    Branches = new List<Branch>() {
                        new Branch {
                            Name = "Bursa Central",
                            City = "Bursa",
                            Code = 16
                        },
                        new Branch {
                            Name = "Sivas",
                            City = "Sivas",
                            Code = 58
                        },
                        new Branch {
                            Name = "Ankara",
                            City = "Ankara",
                            Code = 6
                        },
                        new Branch {
                            Name = "Istanbul",
                            City = "Istanbul",
                            Code = 34
                        },
                    }
                },
                ContactDetail = new Dictionary<string, object>() {
                    { "Phone",985236471},
                    { "Email","info@janecomputer.com"},
                    { "Fax",985236471}
                },
                LuckyNumbers = new List<int>() { 2, 18, 22 },
                MenuItems = new List<string>() { "MainMenu", "Transfers" }
            },
            new Customer()
            {
                Id = 3,
                Name = "Adam Doe",
                Company = new Company()
                {
                    Name = "Super Medicine",
                    FoundDate = new DateTime(2002, 3, 5),
                    Branches = new List<Branch>() {
                        new Branch {
                            Name = "Ankara",
                            City = "Ankara",
                            Code = 6
                        }
                    }
                },
                ContactDetail = new Dictionary<string, object>() {
                    { "Phone",789456123}
                },
                LuckyNumbers = new List<int>() { 7 }
            },
            new Customer()
            {
                Id=4,
                Name = "Madam Doe",
                Company = new Company()
                {
                    Name = "X technic",
                    FoundDate = new DateTime(2001, 3, 5),
                    Branches = new List<Branch>() {
                        new Branch {
                            Name = "Istanbul",
                            City = "Istanbul"
                        }
                    }
                },
                ContactDetail = new Dictionary<string, object>() {
                    { "Email","info@xtechnic.com"}
                }

            });
    }
}