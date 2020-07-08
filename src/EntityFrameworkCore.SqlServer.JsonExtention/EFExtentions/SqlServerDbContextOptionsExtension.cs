using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public class SqlServerDbContextOptionsExtension : IDbContextOptionsExtension {
        private DbContextOptionsExtensionInfo _info;

        public void Validate(IDbContextOptions options) {
        }

        public DbContextOptionsExtensionInfo Info {
            get {
                return this._info ??= (JsonDbContextOptionsExtensionInfo)new JsonDbContextOptionsExtensionInfo((IDbContextOptionsExtension)this);
            }
        }

        void IDbContextOptionsExtension.ApplyServices(IServiceCollection services) {
            services.AddSingleton<IMethodCallTranslatorProvider, SqlServerMethodCallTranslatorPlugin>();
        }

        private sealed class JsonDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo {
            public JsonDbContextOptionsExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "using Json MsSql";

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) {

            }

            public override long GetServiceProviderHashCode() {
                return 0;
            }
        }
    }
}
