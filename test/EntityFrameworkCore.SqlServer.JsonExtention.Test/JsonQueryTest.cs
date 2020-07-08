using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class JsonQueryTest : TestBase {
        [Fact]
        public void JsonQuery_SelectJsonArrayValue_ShouldReturnArray() {
            var entityId = 1;

            var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.LuckyNumbers)).FirstOrDefault();

            result.ShouldBe("[3,8,15]");
        }

        [Fact]
        public void JsonQuery_SelectNestedJsonArrayValueConvertObject_ShouldReturnIstanbul() {
            var entityId = 1;

            var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.Company, "Branches")).FirstOrDefault();

            var branches = JsonSerializer.Deserialize<List<Branch>>(result);

            branches.FirstOrDefault(f => f.Code == 34).City.ShouldBe("Istanbul");
        }
    }
}
