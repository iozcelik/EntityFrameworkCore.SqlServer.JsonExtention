using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class JsonValueTest : TestBase {
        [Fact]
        public void JsonValue_SelectJsonPropertyValue_ShouldReturnJohnsBank() {
            var entityId = 1;

            var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.Company, "Name")).FirstOrDefault();

            result.ShouldBe("John's Bank");
        }

        [Fact]
        public void JsonValue_SelectJsonArrayValue_ShouldReturnStringThree() {
            var entityId = 1;

            var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.LuckyNumbers, 0)).FirstOrDefault();

            result.ShouldBe("3");
        }

        [Fact]
        public void JsonValue_SelectDictionaryValue_ShouldReturnStringPhoneNumber() {
            var entityId = 1;

            var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonValue(s.ContactDetail, "Phone")).FirstOrDefault();

            result.ShouldBe("123456789");
        }

        [Fact]
        public void JsonValue_SelectAllJsonArrayValue_ShouldReturnOneNull() {
            var result = _context.Customers.Select(s => EF.Functions.JsonValue(s.LuckyNumbers, 0)).ToList();

            result.Count(c => c == null).ShouldBe(1);
        }

        [Fact]
        public void JsonValue_WhereDictionaryValueAndSelectAndOrderByDescending_ShouldReturnOneNull() {
            var result = _context.Customers
                .Where(w => EF.Functions.JsonValue(w.ContactDetail, "Phone") != null)
                .OrderByDescending(o => EF.Functions.JsonValue(o.ContactDetail, "Phone"))
                .Select(s => EF.Functions.JsonValue(s.ContactDetail, "Phone"))
                .ToList();

            result.FirstOrDefault().ShouldBe("985236471");
        }
    }
}
