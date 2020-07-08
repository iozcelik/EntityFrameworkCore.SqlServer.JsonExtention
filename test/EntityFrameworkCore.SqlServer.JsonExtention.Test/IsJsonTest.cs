using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using Xunit;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class IsJsonTest : TestBase {
        [Fact]
        public void IsJson_SelectFirstOrDefaultStringNameColumn_ShouldReturnZero() {
            var result = _context.Customers.Select(s => EF.Functions.IsJson(s.Name)).FirstOrDefault();

            result.ShouldBe(0);
        }

        [Fact]
        public void IsJson_SelectFirstOrDefaultEntityCompanyColumn_ShouldReturnOne() {
            var result = _context.Customers.Select(s => EF.Functions.IsJson(s.Company)).FirstOrDefault();

            result.ShouldBe(1);
        }

        [Fact]
        public void IsJson_SelectFirstOrDefaultDictionaryContactDetailColumn_ShouldReturnOne() {
            var result = _context.Customers.Select(s => EF.Functions.IsJson(s.ContactDetail)).FirstOrDefault();

            result.ShouldBe(1);
        }

        [Fact]
        public void IsJson_SelectFirstOrDefaultListLuckyNumbersColumn_ShouldReturnOne() {
            var result = _context.Customers.Select(s => EF.Functions.IsJson(s.LuckyNumbers)).FirstOrDefault();

            result.ShouldBe(1);
        }

        [Fact]
        public void IsJson_SelectFirstOrDefaultListMenuItemsColumn_ShouldReturnOne() {
            var result = _context.Customers.Select(s => EF.Functions.IsJson(s.MenuItems)).FirstOrDefault();

            result.ShouldBe(1);
        }

        [Fact]
        public void IsJson_WhereStringNameColumn_CountShouldReturnZero() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.Name) == 1).ToList();

            result.Count.ShouldBe(0);
        }

        [Fact]
        public void IsNotJson_WhereStringNameColumn_CountShouldReturnFour() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.Name) == 0).ToList();

            result.Count.ShouldBe(4);
        }

        [Fact]
        public void IsJson_WhereDictionaryContactDetailColumn_CountShouldReturnFour() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.ContactDetail) == 1).ToList();

            result.Count.ShouldBe(4);
        }

        [Fact]
        public void IsNotJson_WhereDictionaryContactDetailColumn_CountShouldReturnZero() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.ContactDetail) == 0).ToList();

            result.Count.ShouldBe(0);
        }

        [Fact]
        public void IsJson_WhereListMenuItemsColumn_CountShouldReturnTwo() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.MenuItems) == 1).ToList();

            result.Count.ShouldBe(2);
        }

        [Fact]
        public void IsNotJson_WhereListMenuItemsColumn_CountShouldReturnTwo() {
            var result = _context.Customers.Where(s => EF.Functions.IsJson(s.MenuItems) == null).ToList();

            result.Count.ShouldBe(2);
        }
    }
}
