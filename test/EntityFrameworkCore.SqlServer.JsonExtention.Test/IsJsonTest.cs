using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test;
public class IsJsonTest : IClassFixture<TestDatabaseFixture>
{
    public IsJsonTest(TestDatabaseFixture fixture)
       => Fixture = fixture;
    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public void IsJson_SelectFirstOrDefaultStringNameColumn_ShouldReturnZero()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.IsJson(s.Name)).FirstOrDefault();

        result.ShouldBe(0);
    }

    [Fact]
    public void IsJson_SelectFirstOrDefaultEntityCompanyColumn_ShouldReturnOne()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.IsJson(s.CountryDetail)).FirstOrDefault();

        result.ShouldBe(1);
    }

    [Fact]
    public void IsJson_SelectFirstOrDefaultDictionaryContactDetailColumn_ShouldReturnOne()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.IsJson(s.ExtraInformation)).FirstOrDefault();

        result.ShouldBe(1);
    }

    [Fact]
    public void IsJson_SelectFirstOrDefaultListLuckyNumbersColumn_ShouldReturnOne()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.IsJson(s.UtcTimeZones)).FirstOrDefault();

        result.ShouldBe(1);
    }

    [Fact]
    public void IsJson_SelectFirstOrDefaultListMenuItemsColumn_ShouldReturnOne()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.IsJson(s.OfficialLanguages)).FirstOrDefault();

        result.ShouldBe(1);
    }

    [Fact]
    public void IsJson_WhereStringNameColumn_CountShouldReturnZero()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.Name) == 1).ToList();

        result.Count.ShouldBe(0);
    }

    [Fact]
    public void IsNotJson_WhereStringNameColumn_CountShouldReturnFour()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.Name) == 0).ToList();

        result.Count.ShouldBe(4);
    }

    [Fact]
    public void IsJson_WhereDictionaryContactDetailColumn_CountShouldReturnFour()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.ExtraInformation) == 1).ToList();

        result.Count.ShouldBe(4);
    }

    [Fact]
    public void IsNotJson_WhereDictionaryContactDetailColumn_CountShouldReturnZero()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.ExtraInformation) == 0).ToList();

        result.Count.ShouldBe(0);
    }

    [Fact]
    public void IsJson_WhereListMenuItemsColumn_CountShouldReturnTwo()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.OfficialLanguages) == 1).ToList();

        result.Count.ShouldBe(2);
    }

    [Fact]
    public void IsNotJson_WhereListMenuItemsColumn_CountShouldReturnTwo()
    {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(s => EF.Functions.IsJson(s.OfficialLanguages) == null).ToList();

        result.Count.ShouldBe(2);
    }
}
