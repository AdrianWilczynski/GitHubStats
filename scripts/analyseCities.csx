#load "models.csx"
#load "shared.csx"

using System.Linq;

private var input = Args[0];
private var output = Args[1];

private var cities = LoadDataFile<IEnumerable<City>>(input);

SaveDataFile(output, new Data
{
    Total = CalculateTotals(cities),
    Voivodeships = GroupByVoivodeship(cities),
    Trivia = GetTrivia(cities)
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

private Trivia GetTrivia(IEnumerable<City> cities)
    => new Trivia
    {
        BigCityWithFewestDevelopers = cities
            .Where(c => c.Population >= 500_000)
            .OrderBy(c => c.DeveloperCount)
            .First()
            .Name,
        MediumCityWithFewestDevelopers = cities
            .Where(c => c.Population >= 100_000 && c.Population < 500_000)
            .OrderBy(c => c.DeveloperCount)
            .First()
            .Name,
        SmallestTownWithDeveloper = cities
            .Where(c => c.DeveloperCount > 0)
            .OrderBy(c => c.Population)
            .First()
            .Name,
        BigCityWithBestDeveloperToNonDeveloperRatio = cities
            .Where(c => c.Population >= 500_000)
            .OrderByDescending(c => c.Percentage)
            .First()
            .Name,
        MediumCityWithBestDeveloperToNonDeveloperRatio = cities
            .Where(c => c.Population >= 100_000 && c.Population < 500_000)
            .OrderByDescending(c => c.Percentage)
            .First()
            .Name
    };