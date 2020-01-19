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
    public Trivia Trivia { get; set; }
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

public class Trivia
{
    public string MediumCityWithFewestDevelopers { get; set; }
    public string BigCityWithFewestDevelopers { get; set; }
    public string SmallestTownWithDeveloper { get; set; }
    public string BigCityWithBestDeveloperToNonDeveloperRatio { get; set; }
    public string MediumCityWithBestDeveloperToNonDeveloperRatio { get; set; }
}

public class CityData
{
    public int RepositoryCount { get; set; }
    public int LicensesCount { get; set; }
    public IEnumerable<License> Licenses { get; set; }
    public int LanguagesCount { get; set; }
    public IEnumerable<Language> Languages { get; set; }
    public IEnumerable<Repository> TopRepositories { get; set; }
    public IEnumerable<Year> Years { get; set; }
    public IEnumerable<LanguageOverTheYears> LanguagesOverTheYears { get; set; }
}

public class License
{
    public int Count { get; set; }
    public string Name { get; set; }
}

public class Language
{
    public int Count { get; set; }
    public string Name { get; set; }
    public double StarsAverage { get; set; }
    public double IssuesAverage { get; set; }
}

public class Repository
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int Stars { get; set; }
    public string Language { get; set; }
}

public class Year
{
    public int RepositoriesCreatedCount { get; set; }
    public int Value { get; set; }
}

public class LanguageOverTheYears
{
    public string Name { get; set; }
    public IEnumerable<Year> Years { get; set; }
}