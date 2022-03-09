MsSql Json Extention for Entity Framework Core 6
======================

This extention aim is define json columns and querying them.

### Get it for .Net 6
```
PM> Install-Package EntityFrameworkCore.SqlServer.JsonExtention -Version 2.0.0
```

### Get it for .Net 3.1
```
PM> Install-Package EntityFrameworkCore.SqlServer.JsonExtention -Version 1.0.0
```

### Basic usage
#### Add Option Builder First
Inside dbcontext OnConfiguring method, call UseJsonExtention()

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseJsonFunctions();
}
```

#### Add Json Conversion 
Using Fluent api in model builder and just call HasJsonConversion() method.

```csharp
modelBuilder.Entity<Country>().Property(p => p.CountryDetail).HasJsonConversion();
```

#### Supported Conversion 
The library supports List<>, Dictionary<string,object> and custom entity types.

Example entities:
```csharp
public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CountryDetail CountryDetail { get; set; }
    public Dictionary<string, object> ExtraInformation { get; set; }
    public List<string> OfficialLanguages { get; set; }
    public List<int> UtcTimeZones { get; set; }
}

public class CountryDetail
{
    public string Code { get; set; }
    public DateTime? Founded { get; set; }
    public List<City> Cities { get; set; }
}

public class City
{
    public string Name { get; set; }
    public DateTime? Founded { get; set; }
    public int? Population { get; set; }
}
```

And usage in dbcontext:
```csharp
public class TestContext : DbContext {
    public DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JsonExtentionTest;Integrated Security=True",
            providerOptions => providerOptions.CommandTimeout(60));

        optionsBuilder.UseJsonFunctions();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Country>().Property(p => p.CountryDetail).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.ExtraInformation).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.UtcTimeZones).HasJsonConversion();
        modelBuilder.Entity<Country>().Property(p => p.OfficialLanguages).HasJsonConversion();
    }
}
```

### Querying
Supported Json functions are:
```sql
ISJSON    : Tests whether a string contains valid JSON.
JSON_VALUE: Extracts a scalar value from a JSON string.
JSON_QUERY: Extracts an object or an array from a JSON string.
```
[Mssql documentation](https://docs.microsoft.com/en-us/sql/t-sql/functions/json-functions-transact-sql?view=sql-server-ver15)

#### ISJSON
```csharp
var isJsons = _context.Countries.Select(s => EF.Functions.IsJson(s.CountryDetail)).ToList();
```

Generated SQL query
```sql
SELECT ISJSON([c].[CountryDetail])
FROM [Countries] AS [c]
```

#### JSON_VALUE
```csharp
var phones = _context.Countries
                .Where(w => EF.Functions.JsonValue(w.ExtraInformation, "InternetTLD") != null)
                .OrderByDescending(o => EF.Functions.JsonValue(o.ExtraInformation, "InternetTLD"))
                .Select(s => EF.Functions.JsonValue(s.ExtraInformation, "InternetTLD"))
                .ToList();
```

Generated SQL query
```sql
SELECT JSON_VALUE([c].[ExtraInformation], '$.InternetTLD')
FROM [Countries] AS [c]
WHERE JSON_VALUE([c].[ExtraInformation], '$.InternetTLD') IS NOT NULL
ORDER BY JSON_VALUE([c].[ExtraInformation], '$.InternetTLD') DESC
```

#### JSON_QUERY
```csharp
var entityId = 1;

var result = _context.Countries.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.CountryDetail, "Cities")).FirstOrDefault();

var cities = JsonSerializer.Deserialize<List<City>>(result);
```

Generated SQL query
```sql
SELECT TOP(1) JSON_QUERY([c].[CountryDetail], '$.Cities')
FROM [Countries] AS [c]
WHERE [c].[Id] = 1
```
