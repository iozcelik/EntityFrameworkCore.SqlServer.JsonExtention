MsSql Json Extention for Entity Framework Core 3
======================

This extention aim is define json columns and querying them.

### Get it
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
modelBuilder.Entity<Customer>().Property(p => p.Company).HasJsonConversion();
```

#### Supported Conversion 
The library supports List<>, Dictionary<string,object> and custom entity types.

Example entities:
```csharp
public class Customer {
    public int Id { get; set; }
    public string Name { get; set; }
    public Company Company { get; set; }
    public Dictionary<string,object> ContactDetail { get; set; }
    public List<string> MenuItems { get; set; }
    public List<int> LuckyNumbers { get; set; }
 }

public class Company {
    public string Name { get; set; }
    public DateTime? FoundDate { get; set; }
    public List<Branch> Branches { get; set; }
}

public class Branch {
    public string Name { get; set; }
    public string City { get; set; }
    public int? Code { get; set; }
}
```

And usage in dbcontext:
```csharp
public class TestContext : DbContext {
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JsonExtentionTest;Integrated Security=True",
            providerOptions => providerOptions.CommandTimeout(60));

        optionsBuilder.UseJsonFunctions();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Customer>().Property(p => p.Company).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.ContactDetail).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.LuckyNumbers).HasJsonConversion();
        modelBuilder.Entity<Customer>().Property(p => p.MenuItems).HasJsonConversion();
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
var isJsons = _context.Customers.Select(s => EF.Functions.IsJson(s.Company)).ToList();
```

Generated SQL query
```sql
SELECT ISJSON([c].[Company])
FROM [Customers] AS [c]
```

#### JSON_VALUE
```csharp
var phones = _context.Customers
                .Where(w => EF.Functions.JsonValue(w.ContactDetail, "Phone") != null)
                .OrderByDescending(o => EF.Functions.JsonValue(o.ContactDetail, "Phone"))
                .Select(s => EF.Functions.JsonValue(s.ContactDetail, "Phone"))
                .ToList();
```

Generated SQL query
```sql
SELECT JSON_VALUE([c].[ContactDetail], '$.Phone')
FROM [Customers] AS [c]
WHERE JSON_VALUE([c].[ContactDetail], '$.Phone') IS NOT NULL
ORDER BY JSON_VALUE([c].[ContactDetail], '$.Phone') DESC
```

#### JSON_QUERY
```csharp
var entityId = 1;

var result = _context.Customers.Where(w => w.Id == entityId).Select(s => EF.Functions.JsonQuery(s.Company, "Branches")).FirstOrDefault();

var branches = JsonSerializer.Deserialize<List<Branch>>(result);
```

Generated SQL query
```sql
SELECT TOP(1) JSON_QUERY([c].[Company], '$.Branches')
FROM [Customers] AS [c]
WHERE [c].[Id] = 1
```

### RoadMap
- Add to lambda expression to nested json object
- Return type may match to entity type. (At this time only string return avaible)
- JSON_MODIFY function implementation
