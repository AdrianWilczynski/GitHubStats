#r "nuget: Flurl.Http, 2.4.2"
#r "nuget: Newtonsoft.Json, 12.0.3"

#load "env.csx"
#load "models.csx"
#load "citiesInPoland.csx"

using Flurl;
using Flurl.Http;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

private const int RequestsPerMinuteSearchApiRateLimit = 30;

private var userName = Args[0];
private var token = Args[1];

private var city = Args[2];

SaveDataFile($"devsIn{city}", await GetUsers(city, userName, token));

private static async Task<IEnumerable<string>> GetUsers(string city, string userName, string token)
{
    HttpResponseMessage response;
    int requestCount = 0;

    var cityNames = new[] { city }.Concat(GetEnglishNames(city));

    var users = new List<string>();

    foreach (var name in cityNames)
    {
        int page = 0;

        WriteLine(name);

        do
        {
            if (requestCount == RequestsPerMinuteSearchApiRateLimit)
            {
                WriteLine("--- Waiting for rate limit refresh ---");
                await Task.Delay(TimeSpan.FromMinutes(1));
                requestCount = 0;
            }

            page++;
            requestCount++;

            WriteLine($"Requesting page: {page}");

            response = await "https://api.github.com/search/users"
                .SetQueryParams(new { q = $@"location:""{name}""", page })
                .WithHeader("User-Agent", "GitHubStats")
                .WithBasicAuth(userName, token)
                .GetAsync();

            var usersFromPage = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            users.AddRange(((IEnumerable<dynamic>)usersFromPage.items).Select(i => (string)i.login));
        }
        while (HasNextPage(response));
    }

    return users;
}

private static bool HasNextPage(HttpResponseMessage response)
    => response.Headers
        .FirstOrDefault(h => h.Key == "Link").Value
        .Any(l => l.Contains("rel=\"next\""));