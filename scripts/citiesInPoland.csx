#pragma warning disable RCS1090

#r "nuget: AngleSharp,  0.13.0"
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "nuget: morelinq, 3.2.0"

#load "shared.csx"

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
        City = SquashWhitespace(row.QuerySelector("td:first-child").TextContent),
        Voivodeship = SquashWhitespace(row.QuerySelector("td:nth-child(3)").TextContent),
        Population = int.Parse(row.QuerySelector("td:nth-child(5)").TextContent)
    })
    .OrderByDescending(c => c.Population)
    .Take(100)
    .Pipe(c => WriteLine($"City: {c.City}, Voivodeship: {c.Voivodeship}, Population: {c.Population}"));

File.WriteAllText(
    GetDataFilePath("citiesInPoland.json"),
    JsonConvert.SerializeObject(cities, Formatting.Indented));