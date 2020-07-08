using System;

namespace EntityFrameworkCore.SqlServer.JsonExtention.Test {
    public class TestBase : IDisposable {
        protected readonly TestContext _context;

        public TestBase() {
            _context = TestContextFactory.Create();
        }

        public void Dispose() {
            TestContextFactory.Destroy(_context);
        }
    }
}
