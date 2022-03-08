using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention; 
#pragma warning disable EF1001
public sealed class SqlServerMethodCallTranslatorPlugin : SqlServerMethodCallTranslatorProvider {
#pragma warning restore EF1001
    public SqlServerMethodCallTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies)
#pragma warning disable EF1001
        : base(dependencies) {
#pragma warning restore EF1001
        ISqlExpressionFactory jsonSqlExpressionFactory = dependencies.SqlExpressionFactory;

        AddTranslators(new List<IMethodCallTranslator>
        {
            new JsonTranslator(jsonSqlExpressionFactory)
        });
    }
}
