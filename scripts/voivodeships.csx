#load "models.csx"
#load "env.csx"

using System.Linq;

private var cities = LoadDataFile<IEnumerable<City>>("cities");

private var voivodeships = cities
    .GroupBy(c => c.Voivodeship)
    .Select(v => new Voivodeship
    {
        Name = v.Key,
        DeveloperCount = v.Sum(c => c.DeveloperCount)
    });

SaveDataFile("voivodeships", voivodeships);