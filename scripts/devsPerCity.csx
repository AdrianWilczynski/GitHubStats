#r "nuget: Flurl.Http, 2.4.2"
#r "nuget: System.Linq.Async, 4.0.0"

#load "shared.csx"
#load "models.csx"
#load "citiesInPoland.csx"

using Flurl;
using Flurl.Http;
using System.Linq;

private const int RequestsPerMinuteRateLimit = 30;

private var userName = Args[0];
private var token = Args[1];

private async IAsyncEnumerable<City> GetDevCount(IEnumerable<City> cities)
{
    var requestCount = 0;

    foreach (var city in cities)
    {
        WriteLine($"Requesting developer count for: {city.Name}");

        var names = new[] { city.Name }.Concat(city.EnglishNames);
        var totalCount = 0;

        foreach (var name in names)
        {
            if (requestCount == RequestsPerMinuteRateLimit)
            {
                WriteLine("--- Waiting for rate limit refresh ---");

                await Task.Delay(TimeSpan.FromMinutes(1));
                requestCount = 0;
            }

            var response = await "https://api.github.com/search/users"
                .SetQueryParams(new { q = $@"location:""{name}""" })
                .WithHeader("User-Agent", "GitHubStats")
                .WithBasicAuth(userName, token)
                .GetJsonAsync();

            var count = response.total_count;
            totalCount += count;

            requestCount++;

            WriteLine($"Name = {name}, Count = {count}, Total Count = {totalCount}, Request = {requestCount}");
        }

        city.DeveloperCount = totalCount;
        city.Percentage = Math.Round((totalCount / (double)city.Population) * 100, 5);

        yield return city;
    }
}

private var cites = (await GetCitiesAsync());
private var citiesWithDevCount = await GetDevCount(cites).ToListAsync();

SaveDataFile("cities", citiesWithDevCount);