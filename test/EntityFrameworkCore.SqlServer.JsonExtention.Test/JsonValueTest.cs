using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test; 
public class JsonValueTest : IClassFixture<TestDatabaseFixture> {
    public JsonValueTest(TestDatabaseFixture fixture)
       => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public void JsonValue_SelectJsonPropertyValue_ShouldReturnJohnsBank() {
        var entityId = 1;

        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.CountryDetail, "Code")).FirstOrDefault();

        result.ShouldBe("TR");
    }

    [Fact]
    public void JsonValue_SelectJsonArrayValue_ShouldReturnStringThree() {
        var entityId = 1;
        using var _context = Fixture.CreateContext();

        var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.UtcTimeZones, 0)).FirstOrDefault();

        result.ShouldBe("3");
    }

    [Fact]
    public void JsonValue_SelectDictionaryValue_ShouldReturnStringPhoneNumber() {
        var entityId = 1;

        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.ExtraInformation, "InternetTLD")).FirstOrDefault();

        result.ShouldBe(".tr");
    }

    [Fact]
    public void JsonValue_SelectAllJsonArrayValue_ShouldReturnOneNull() {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Select(s => EF.Functions.JsonValue(s.UtcTimeZones, 0)).ToList();

        result.Count(c => c == null).ShouldBe(1);
    }

    [Fact]
    public void JsonValue_WhereDictionaryValueAndSelectAndOrderByDescending_ShouldReturnOneNull() {
        using var _context = Fixture.CreateContext();
        var result = _context.Customers
            .Where(w => EF.Functions.JsonValue(w.ExtraInformation, "InternetTLD") != null)
            .OrderByDescending(o => EF.Functions.JsonValue(o.ExtraInformation, "InternetTLD"))
            .Select(s => EF.Functions.JsonValue(s.ExtraInformation, "InternetTLD"))
            .ToList();

        result.FirstOrDefault().ShouldBe(".us");
    }
}
