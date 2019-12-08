#pragma warning disable RCS1090

#r "nuget: AngleSharp,  0.13.0"
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "nuget: morelinq, 3.2.0"

#load "shared.csx"
#load "englishCityNames.csx"

using System.Text.RegularExpressions;
using AngleSharp;
using MoreLinq;
using Newtonsoft.Json;

private static string SquashWhitespace(string text)
    => Regex.Replace(text, @"\s+", " ")
        .Trim();

private var document = await BrowsingContext.New(Configuration.Default.WithDefaultLoader())
    .OpenAsync("https://pl.wikipedia.org/wiki/Dane_statystyczne_o_miastach_w_Polsce");

private var cities = document.QuerySelectorAll("table.sortable.wikitable tr:not(:first-child)")
    .Select(row => new
    {
        Name = SquashWhitespace(row.QuerySelector("td:first-child").TextContent),
        Voivodeship = SquashWhitespace(row.QuerySelector("td:nth-child(3)").TextContent),
        Population = int.Parse(row.QuerySelector("td:nth-child(5)").TextContent)
    })
    .OrderByDescending(c => c.Population)
    .Take(100)
    .Select(c => new
    {
        c.Name,
        EnglishNames = GetEnglishNames(c.Name)
            .Where(n => n != c.Name),
        c.Voivodeship,
        c.Population
    })
    .Pipe(c => WriteLine($"Name: {c.Name}, English Names: {string.Join(", ", c.EnglishNames)},"
        + $"Voivodeship: {c.Voivodeship}, Population: {c.Population}"));

File.WriteAllText(
    GetDataFilePath("citiesInPoland.json"),
    JsonConvert.SerializeObject(cities, Formatting.Indented));