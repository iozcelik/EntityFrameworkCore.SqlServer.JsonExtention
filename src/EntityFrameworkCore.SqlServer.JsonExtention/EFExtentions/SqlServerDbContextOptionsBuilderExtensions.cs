using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public static class SqlServerDbContextOptionsBuilderExtensions {
        public static DbContextOptionsBuilder UseJsonFunctions(
            this DbContextOptionsBuilder optionsBuilder) {
            var extension = (SqlServerDbContextOptionsExtension)GetOrCreateExtension(optionsBuilder);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        private static SqlServerDbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<SqlServerDbContextOptionsExtension>()
               ?? new SqlServerDbContextOptionsExtension();
    }
}
