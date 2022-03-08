using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace EntityFrameworkCore.SqlServer.JsonExtention;

public class JsonTranslator : IMethodCallTranslator {
    private readonly ISqlExpressionFactory _expressionFactory;

    public JsonTranslator(ISqlExpressionFactory sqlExpressionFactory) {
        _expressionFactory = sqlExpressionFactory;

    }

    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        if (method == null)
            throw new ArgumentNullException(nameof(method));
        if (arguments == null)
            throw new ArgumentNullException(nameof(arguments));


        switch (method.Name)
        {
            case nameof(DbFunctionsExtensions.IsJson):
                {
                    var args = new List<SqlExpression> { arguments[1] };
                    return _expressionFactory.Function("ISJSON", args, true, new[] { true }, typeof(int));
                }
            case nameof(DbFunctionsExtensions.JsonValue) when arguments[2].Type != typeof(int):
                {
                    var expr = new SqlFragmentExpression("'$." + ((SqlConstantExpression)arguments[2]).Value.ToString() + "'");
                    var args = new List<SqlExpression> { arguments[1], expr };
                    return _expressionFactory.Function("JSON_VALUE", args, true, new[] { true, true }, typeof(string));
                }
            case nameof(DbFunctionsExtensions.JsonValue) when arguments[2].Type == typeof(int):
                {
                    var expr = new SqlFragmentExpression("'$[" + ((SqlConstantExpression)arguments[2]).Value.ToString() + "]'");
                    var args = new List<SqlExpression> { arguments[1], expr };
                    return _expressionFactory.Function("JSON_VALUE", args, true, new[] { true, true }, typeof(string));
                }
            case nameof(DbFunctionsExtensions.JsonQuery) when arguments.Count < 3:
                {
                    var expr = new SqlFragmentExpression("'$'");
                    var args = new List<SqlExpression> { arguments[1], expr };
                    return _expressionFactory.Function("JSON_QUERY", args, true, new[] { true }, typeof(string));
                }
            case nameof(DbFunctionsExtensions.JsonQuery) when arguments.Count == 3:
                {
                    var expr = new SqlFragmentExpression("'$." + ((SqlConstantExpression)arguments[2]).Value.ToString() + "'");
                    var args = new List<SqlExpression> { arguments[1], expr };
                    return _expressionFactory.Function("JSON_QUERY", args, true, new[] { true, true }, typeof(string));
                }
        }

        return null;
    }
}

