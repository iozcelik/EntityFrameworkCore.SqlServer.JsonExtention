using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test; 
public class JsonQueryTest : IClassFixture<TestDatabaseFixture>
{
    public JsonQueryTest(TestDatabaseFixture fixture)
       => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }
    [Fact]
    public void JsonQuery_SelectJsonArrayValue_ShouldReturnArray() {
        var entityId = 1;

        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.UtcTimeZones)).FirstOrDefault();

        result.ShouldBe("[3]");
    }

    [Fact]
    public void JsonQuery_SelectNestedJsonArrayValueConvertObject_ShouldReturnIstanbul() {
        var entityId = 1;

        using var _context = Fixture.CreateContext();
        var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.CountryDetail, "Cities")).FirstOrDefault();

        var cities = JsonSerializer.Deserialize<List<City>>(result);

        cities.FirstOrDefault(f => f.Population == 15840900).Name.ShouldBe("Istanbul");
    }
}
