#load "models.csx"
#load "shared.csx"

using System.Linq;

private var input = Args[0];
private var output = Args[1];

private var cities = LoadDataFile<IEnumerable<City>>(input);

SaveDataFile(output, new Data
{
    Total = CalculateTotals(cities),
    Voivodeships = GroupByVoivodeship(cities)
});

private Total CalculateTotals(IEnumerable<City> cities)
{
    var developerCount = cities.Sum(c => c.DeveloperCount);
    var population = cities.Sum(c => c.Population);

    return new Total
    {
        DeveloperCount = developerCount,
        Population = population,
        Percentage = Math.Round((developerCount / (double)population) * 100, 2)
    };
}

private IEnumerable<Voivodeship> GroupByVoivodeship(IEnumerable<City> cities)
    => cities.GroupBy(c => c.Voivodeship)
        .Select(v => new Voivodeship
        {
            Name = v.Key,
            DeveloperCount = v.Sum(c => c.DeveloperCount)
        });