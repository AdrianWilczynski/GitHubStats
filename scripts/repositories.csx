#r "nuget: Flurl.Http, 2.4.2"
#r "nuget: Newtonsoft.Json, 12.0.3"

#load "shared.csx"

using Flurl;
using Flurl.Http;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

private var userName = Args[0];
private var token = Args[1];

private var input = Args[2];

private var logins = LoadDataFile<IEnumerable<string>>(input);

private var repositories = new Dictionary<string, object>();

foreach (var login in logins)
{
    HttpResponseMessage response;
    var page = 0;

    var repositoriesPerLogin = new List<object>();

    WriteLine(login);

    do
    {
        page++;

        WriteLine($"Requesting page: {page}");

        response = await $"https://api.github.com/users/{login}/repos"
          .SetQueryParams(new { page })
          .WithHeader("User-Agent", "GitHubStats")
          .WithBasicAuth(userName, token)
          .GetAsync();

        var repositoriesPerPage = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync());
        repositoriesPerLogin.AddRange(repositoriesPerPage);

    } while (HasNextPage(response));

    repositories[login] = repositoriesPerLogin;
}

SaveDataFile($"{input}-repositories", repositories);