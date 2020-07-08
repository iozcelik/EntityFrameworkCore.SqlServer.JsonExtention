using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public sealed class SqlServerMethodCallTranslatorPlugin : SqlServerMethodCallTranslatorProvider {
        public SqlServerMethodCallTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies)
            : base(dependencies) {
            var jsonSqlExpressionFactory = dependencies.SqlExpressionFactory;

            this.AddTranslators(new List<IMethodCallTranslator>
            {
                new JsonTranslator(jsonSqlExpressionFactory)
            });
        }
    }
}
