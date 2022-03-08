using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.SqlServer.JsonExtention; 
public static class SqlServerDbContextOptionsBuilderExtensions {
    public static DbContextOptionsBuilder UseJsonFunctions(
        this DbContextOptionsBuilder optionsBuilder) {
        var extension = GetOrCreateExtension(optionsBuilder);
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    private static SqlServerJsonOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.Options.FindExtension<SqlServerJsonOptionsExtension>()
           ?? new SqlServerJsonOptionsExtension();
}
