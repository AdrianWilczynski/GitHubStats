#pragma warning disable RCS1110

public class City
{
    public string Name { get; set; }
    public IEnumerable<string> EnglishNames { get; set; }
    public string Voivodeship { get; set; }
    public int Population { get; set; }
    public int DeveloperCount { get; set; }
    public double Percentage { get; set; }
}

public class Data
{
    public Total Total { get; set; }
    public IEnumerable<Voivodeship> Voivodeships { get; set; }
}

public class Voivodeship
{
    public string Name { get; set; }
    public int DeveloperCount { get; set; }
}

public class Total
{
    public int DeveloperCount { get; set; }
    public int Population { get; set; }
    public double Percentage { get; set; }
}