using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public static class ValueConversionExtensions {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new() {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true }),
                v => JsonSerializer.Deserialize<T>(v, new JsonSerializerOptions { IgnoreNullValues = true }) ?? new T()
            );

            ValueComparer<T> comparer = new ValueComparer<T>
            (
                (l, r) => JsonSerializer.Serialize(l, new JsonSerializerOptions { IgnoreNullValues = true }) == JsonSerializer.Serialize(r, new JsonSerializerOptions { IgnoreNullValues = true }),
                v => v == null ? 0 : JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true }).GetHashCode(),
                v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true }), new JsonSerializerOptions { IgnoreNullValues = true })
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);

            return propertyBuilder;
        }
    }

}
