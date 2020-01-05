#r "nuget: AngleSharp,  0.13.0"

#load "models.csx"

using System.Text.RegularExpressions;
using AngleSharp;

public async Task<IEnumerable<City>> GetCitiesAsync()
{
    static string SquashWhitespace(string text) => Regex.Replace(text, @"\s+", " ").Trim();

    var document = await BrowsingContext.New(Configuration.Default.WithDefaultLoader())
       .OpenAsync("https://pl.wikipedia.org/wiki/Dane_statystyczne_o_miastach_w_Polsce");

    return (from row in document.QuerySelectorAll("table.sortable.wikitable tr:not(:first-child)")
            let name = SquashWhitespace(row.QuerySelector("td:first-child").TextContent)
            let population = int.Parse(row.QuerySelector("td:nth-child(5)").TextContent)
            orderby population descending
            select new City
            {
                Name = name,
                EnglishNames = GetEnglishNames(name),
                Voivodeship = SquashWhitespace(row.QuerySelector("td:nth-child(3)").TextContent),
                Population = population
            }).ToList();
}

private IEnumerable<string> GetEnglishNames(string name)
{
    var HardcodedNames = new Dictionary<string, string>
    {
        { "Warszawa", "Warsaw" },
        { "Kraków", "Cracow" }
    };

    if (HardcodedNames.TryGetValue(name, out var hardcodedName))
        yield return hardcodedName;

    static string RemovePolishCharacters(string name)
        => name.Replace("ą", "a").Replace("Ą", "A")
            .Replace("ć", "c").Replace("Ć", "C")
            .Replace("ę", "e").Replace("Ę", "E")
            .Replace("ł", "l").Replace("Ł", "L")
            .Replace("ń", "n").Replace("Ń", "N")
            .Replace("ó", "o").Replace("Ó", "O")
            .Replace("ś", "s").Replace("Ś", "S")
            .Replace("ź", "z").Replace("Ź", "Z")
            .Replace("ż", "z").Replace("Ż", "Z");

    if (RemovePolishCharacters(name) is var withRemovedPolishChracters && withRemovedPolishChracters != name)
        yield return withRemovedPolishChracters;
}