using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace EntityFrameworkCore.SqlServer.JsonExtention {
    //public class JsonSqlExpressionFactory : SqlExpressionFactory {
    //    readonly IRelationalTypeMappingSource _typeMappingSource;
    //    readonly RelationalTypeMapping _stringTypeMapping;

    //    public JsonSqlExpressionFactory(SqlExpressionFactoryDependencies dependencies) : base(dependencies) {
    //        Check.NotNull(dependencies, nameof(dependencies));

    //        _typeMappingSource = dependencies.TypeMappingSource;
    //        _stringTypeMapping = _typeMappingSource.FindMapping(typeof(string));
    //    }

    //    public virtual JsonValueExpression JsonValue(SqlExpression match, SqlExpression pattern) {
    //        Check.NotNull(match, nameof(match));
    //        Check.NotNull(pattern, nameof(pattern));

    //        return (JsonValueExpression)ApplyDefaultTypeMapping(new JsonValueExpression(match, pattern, _stringTypeMapping));
    //    }
    //}



    public class JsonTranslator : IMethodCallTranslator {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public JsonTranslator(ISqlExpressionFactory sqlExpressionFactory) {
            _sqlExpressionFactory = sqlExpressionFactory;

        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments) {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));


            switch (method.Name) {
                case nameof(DbFunctionsExtensions.IsJson): {
                        var args = new List<SqlExpression> { arguments[1] };
                        return _sqlExpressionFactory.Function(instance, "ISJSON", args, typeof(int));
                    }
                case nameof(DbFunctionsExtensions.JsonValue) when arguments[2].Type != typeof(int): {
                        var expr = new SqlFragmentExpression("'$." + ((SqlConstantExpression)arguments[2]).Value.ToString() + "'");
                        var args = new List<SqlExpression> { arguments[1], expr };
                        return _sqlExpressionFactory.Function(instance, "JSON_VALUE", args, typeof(string));
                    }
                case nameof(DbFunctionsExtensions.JsonValue) when arguments[2].Type == typeof(int): {
                        var expr = new SqlFragmentExpression("'$[" + ((SqlConstantExpression)arguments[2]).Value.ToString() + "]'");
                        var args = new List<SqlExpression> { arguments[1], expr };
                        return _sqlExpressionFactory.Function(instance, "JSON_VALUE", args, typeof(string));
                    }
                case nameof(DbFunctionsExtensions.JsonQuery) when arguments.Count < 3: {
                        var expr = new SqlFragmentExpression("'$'");
                        var args = new List<SqlExpression> { arguments[1], expr };
                        return _sqlExpressionFactory.Function(instance, "JSON_QUERY", args, typeof(string));
                    }
                case nameof(DbFunctionsExtensions.JsonQuery) when arguments.Count == 3: {
                        var expr = new SqlFragmentExpression("'$." + ((SqlConstantExpression)arguments[2]).Value.ToString() + "'");
                        var args = new List<SqlExpression> { arguments[1], expr };
                        return _sqlExpressionFactory.Function(instance, "JSON_QUERY", args, typeof(string));
                    }
            }

            return null;
        }

    }

    //public class JsonValueExpression : SqlExpression {
    //    public virtual SqlExpression Column { get; }
    //    public virtual SqlExpression Pattern { get; }

    //    public JsonValueExpression(
    //         SqlExpression column,
    //         SqlExpression pattern,
    //         RelationalTypeMapping typeMapping)
    //        : base(typeof(string), typeMapping) {
    //        Check.NotNull(column, nameof(column));
    //        Check.NotNull(pattern, nameof(pattern));

    //        Column = column;
    //        Pattern = pattern;
    //    }

    //    protected override Expression VisitChildren(ExpressionVisitor visitor) {
    //        Check.NotNull(visitor, nameof(visitor));

    //        var column = (SqlExpression)visitor.Visit(Column);
    //        var pattern = (SqlExpression)visitor.Visit(Pattern);

    //        return Update(column, Pattern);
    //    }

    //    public virtual JsonValueExpression Update(SqlExpression column, SqlExpression pattern) {
    //        Check.NotNull(column, nameof(column));

    //        return column != Column || pattern != Pattern
    //            ? new JsonValueExpression(column, Pattern, TypeMapping)
    //            : this;
    //    }

    //    public override void Print(ExpressionPrinter expressionPrinter) {
    //        Check.NotNull(expressionPrinter, nameof(expressionPrinter));

    //        expressionPrinter.Visit(Column);
    //        expressionPrinter.Append(" LIKE ");
    //        expressionPrinter.Visit(Pattern);
    //    }

    //    /// <inheritdoc />
    //    public override bool Equals(object obj)
    //        => obj != null
    //            && (ReferenceEquals(this, obj)
    //                || obj is JsonValueExpression likeExpression
    //                && Equals(likeExpression));

    //    private bool Equals(JsonValueExpression likeExpression)
    //        => base.Equals(likeExpression)
    //            && Column.Equals(likeExpression.Column)
    //            && Pattern.Equals(likeExpression.Pattern);

    //    /// <inheritdoc />
    //    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Column, Pattern);
    //}
}
