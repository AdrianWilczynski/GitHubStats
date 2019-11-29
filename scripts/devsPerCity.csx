#pragma warning disable RCS1090

#r "nuget: Newtonsoft.Json, 12.0.3"
#r "nuget: Flurl.Http, 2.4.2"
#r "nuget: morelinq, 3.2.0"
#r "nuget: System.Linq.Async, 4.0.0"

#load "shared.csx"

using Flurl;
using Flurl.Http;
using MoreLinq;
using Newtonsoft.Json;

private const int RequestsPerMinuteRateLimit = 30;

private var userName = Args[0];
private var token = Args[1];

private var cities = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(
        File.ReadAllText(GetDataFilePath("citiesInPoland.json")))
    .Select(c => (string)c.City);

private async IAsyncEnumerable<dynamic> GetDevCount(IEnumerable<string> cities)
{
    var batches = cities.Batch(RequestsPerMinuteRateLimit);
    foreach (var (index, batch) in batches.Index())
    {
        foreach (var city in batch)
        {
            var response = await "https://api.github.com/search/users"
                .SetQueryParams(new { q = $@"location:""{city}""" })
                .WithHeader("User-Agent", "GitHubStats")
                .WithBasicAuth(userName, token)
                .GetJsonAsync();

            var devCount = response.total_count;

            WriteLine($"City: {city}, DevCount: {devCount}");
            yield return new { City = city, DevCount = devCount };
        }

        if (index != batches.Count() - 1)
        {
            WriteLine("--- Waiting for rate limit refresh ---");
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}

File.WriteAllText(
    GetDataFilePath("devsPerCity.json"),
    JsonConvert.SerializeObject(await GetDevCount(cities).ToListAsync(), Formatting.Indented))