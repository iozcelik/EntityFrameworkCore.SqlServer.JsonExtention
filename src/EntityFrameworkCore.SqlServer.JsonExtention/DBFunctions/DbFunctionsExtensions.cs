using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public static class DbFunctionsExtensions {
        public static int? IsJson(this DbFunctions _, string json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static int? IsJson<TKey, TValue>(this DbFunctions _, Dictionary<TKey, TValue> json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static int? IsJson<T>(this DbFunctions _, T json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static int? IsJson<T>(this DbFunctions _, List<T> json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonValue<TKey, TValue>(this DbFunctions _, Dictionary<TKey, TValue> json, string key) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonValue<T>(this DbFunctions _, T json, string key) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonValue<T>(this DbFunctions _, T json, int key) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonQuery<TKey, TValue>(this DbFunctions _, Dictionary<TKey, TValue> json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonQuery<T>(this DbFunctions _, T json) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string JsonQuery<T>(this DbFunctions _, T json, string nestedArrayName) {
            throw new InvalidOperationException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }
    }
}
