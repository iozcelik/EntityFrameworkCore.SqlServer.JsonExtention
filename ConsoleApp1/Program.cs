using ConsoleApp1;
using EntityFrameworkCore.SqlServer.JsonExtention;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

ILoggerFactory myLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
var context = new TestContext(myLoggerFactory);
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
var result = context.Customers.Select(s => EF.Functions.IsJson(s.Name)).FirstOrDefault();
Console.WriteLine();
Console.ReadKey();
