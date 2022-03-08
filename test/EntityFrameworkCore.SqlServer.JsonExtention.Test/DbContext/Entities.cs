using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention;
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