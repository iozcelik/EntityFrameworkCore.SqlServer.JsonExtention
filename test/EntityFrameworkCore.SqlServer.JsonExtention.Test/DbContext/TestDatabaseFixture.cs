using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test;
public class TestDatabaseFixture
{
    private const string ConnectionString = @"Server=.;Database=MssqlJsonTest;Trusted_Connection=True";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(
                        new Country()
                        {
                            Name = "Türkiye",
                            CountryDetail = new CountryDetail()
                            {
                                Code = "TR",
                                Founded = new DateTime(1923, 10, 29),
                                Cities = new List<City>() {
                        new City {
                            Name = "Ankara",
                            Population = 5747325
                        },
                        new City {
                            Name = "Istanbul",
                            Population = 15840900

                        },
                        new City {
                            Name = "Sivas",
                            Population = 388079
                        },
                        new City {
                            Name = "Kırıkkale",
                            Founded = new DateTime(1920,1,1),
                            Population = 275968
                        },
                    }
                            },
                            ExtraInformation = new Dictionary<string, object>() {
                    { "Callingcode",90},
                    { "InternetTLD",".tr"}
                },
                            UtcTimeZones = new List<int>() { 3 },
                            OfficialLanguages = new List<string>() { "Turkish" }
                        },
                        new Country()
                        {
                            Name = "United States Of America",
                            CountryDetail = new CountryDetail()
                            {
                                Code = "US",
                                Founded = new DateTime(1776, 7, 4),
                                Cities = new List<City>() {
                        new City {
                            Name = "Washington, D.C.",
                            Founded = new DateTime(1790, 1, 1),
                            Population = 689545
                        },
                        new City {
                            Name = "New York City",
                            Founded = new DateTime(1624, 1, 1),
                            Population = 8804190
                        },
                        new City {
                            Name = "Sacramento",
                            Founded = new DateTime(1850, 2, 27),
                            Population = 524943
                        },
                        new City {
                            Name = "Seattle",
                            Founded = new DateTime(1851, 11, 13),
                            Population = 737015
                        },
                    }
                            },
                            ExtraInformation = new Dictionary<string, object>() {
                    { "Callingcode",1},
                    { "InternetTLD",".us"},
                    { "Laststateadmitted",1959}
                },
                            UtcTimeZones = new List<int>() { -4, -5, -6, -7, -8, -9, -10, -11, -12, 10, 11 },
                            OfficialLanguages = new List<string>() { "English (de facto)" }
                        },
                        new Country()
                        {
                            Name = "Singapore",
                            CountryDetail = new CountryDetail()
                            {
                                Code = "SG",
                                Founded = new DateTime(1959, 6, 3),
                                Cities = new List<City>() {
                        new City {
                            Name = "Singapore",
                            Founded = new DateTime(1299, 1, 1),
                            Population = 5453600
                        }
                    }
                            },
                            ExtraInformation = new Dictionary<string, object>() {
                    { "Callingcode",65}
                },
                            UtcTimeZones = new List<int>() { 8 }
                        },
                        new Country()
                        {
                            Name = "Kenya",
                            CountryDetail = new CountryDetail()
                            {
                                Code = "KE",
                                Founded = new DateTime(1963, 12, 12),
                                Cities = new List<City>() {
                        new City {
                            Name = "Nairobi",
                            Founded = new DateTime(1899, 1, 1)
                        }
                    }
                            },
                            ExtraInformation = new Dictionary<string, object>() {
                    { "InternetTLD",".ke"}
                }
                        }
                        );
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public TestContext CreateContext()
        => new TestContext(
            new DbContextOptionsBuilder<TestContext>()
                .UseSqlServer(ConnectionString)
                .Options);
}