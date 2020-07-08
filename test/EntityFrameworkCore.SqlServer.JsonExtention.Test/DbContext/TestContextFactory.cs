using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class TestContextFactory {
        public static TestContext Create() {
            var context = new TestContext();

            context.Database.EnsureCreated();

            var count = context.Customers.Count();
            if (count == 0) {
                var customer1 = new Customer() {
                    Name = "John Doe",
                    Company = new Company() {
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
                };
                var customer2 = new Customer() {
                    Name = "Jane Doe",
                    Company = new Company() {
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
                };
                var customer3 = new Customer() {
                    Name = "Adam Doe",
                    Company = new Company() {
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
                };
                var customer4 = new Customer() {
                    Name = "Madam Doe",
                    Company = new Company() {
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
                };
                context.Customers.Add(customer1);
                context.Customers.Add(customer2);
                context.Customers.Add(customer3);
                context.Customers.Add(customer4);
                context.SaveChanges();
            }

            return context;
        }

        public static void Destroy(TestContext context) {
            context.Dispose();
        }
    }
}
