using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention;

public class SqlServerJsonOptionsExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public void Validate(IDbContextOptions options)
    {
    }

    public virtual DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

    public virtual void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<IMethodCallTranslatorProvider, SqlServerMethodCallTranslatorPlugin>();


    }

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        public ExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

        private new SqlServerJsonOptionsExtension Extension
            => (SqlServerJsonOptionsExtension)base.Extension;

        public override bool IsDatabaseProvider => false;

        public override string LogFragment => "using EfCoreJson";

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {

        }

        public override int GetServiceProviderHashCode() => 0;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => other is ExtensionInfo;
    }
}
