#pragma warning disable IDE1006, RCS1110

#load "shared.csx"
#load "models.csx"

using System.Linq;

private class RepositoryFromApi
{
    public string name { get; set; }
    public string html_url { get; set; }
    public string description { get; set; }
    public string language { get; set; }
    public LicenseFromApi license { get; set; }
    public int open_issues { get; set; }
    public int forks { get; set; }
    public int watchers { get; set; }
    public int stargazers_count { get; set; }
    public bool has_pages { get; set; }
    public bool has_wiki { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}

private class LicenseFromApi
{
    public string key { get; set; }
    public string name { get; set; }
}

private var input = Args[0];
private var output = Args[1];

private var repositoriesPerUser = LoadDataFile<Dictionary<string, IEnumerable<RepositoryFromApi>>>(input);

private var licenses = repositoriesPerUser
    .SelectMany(u => u.Value)
    .GroupBy(r => r.license?.name ?? "None")
    .Select(g => new License
    {
        Name = g.Key,
        Count = g.Count()
    })
    .OrderByDescending(l => l.Count);

private var languages = repositoriesPerUser
    .SelectMany(u => u.Value)
    .Where(r => r.language != null)
    .GroupBy(r => r.language)
    .Select(g => new Language
    {
        Name = g.Key,
        Count = g.Count(),
        StarsAverage = repositoriesPerUser
            .SelectMany(u => u.Value)
            .Where(r => r.language == g.Key)
            .Average(r => r.stargazers_count),
        IssuesAverage = repositoriesPerUser
            .SelectMany(u => u.Value)
            .Where(r => r.language == g.Key)
            .Average(r => r.open_issues)
    })
    .OrderByDescending(l => l.Count);

private var topRepositories = repositoriesPerUser
    .SelectMany(u => u.Value)
    .OrderByDescending(r => r.stargazers_count)
    .Take(10)
    .Select(r => new Repository
    {
        Name = r.name,
        Url = r.html_url,
        Description = r.description,
        Stars = r.stargazers_count,
        Language = r.language ?? "-"
    });

private const int gitHubCreationYear = 2008;
private var githubActiveYears = Enumerable.Range(gitHubCreationYear, DateTime.Now.Year + 1 - gitHubCreationYear);

private var years = githubActiveYears
    .Select(y => new Year
    {
        Value = y,
        RepositoriesCreatedCount = repositoriesPerUser
            .SelectMany(u => u.Value)
            .Count(r => r.created_at.Year == y)
    });

private var top10Languages = repositoriesPerUser
    .SelectMany(u => u.Value)
    .Where(r => r.language != null)
    .GroupBy(r => r.language)
    .OrderByDescending(g => g.Count())
    .Take(10)
    .Select(g => g.Key);

private var languagesOverTheYears = top10Languages
    .Select(l => new LanguageOverTheYears
    {
        Name = l,
        Years = githubActiveYears
            .Select(y => new Year
            {
                Value = y,
                RepositoriesCreatedCount = repositoriesPerUser
                    .SelectMany(r => r.Value)
                    .Count(r => r.language == l && r.created_at.Year == y)
            })
    });

SaveDataFile(output, new CityData
{
    RepositoryCount = repositoriesPerUser.Sum(u => u.Value.Count()),
    LicensesCount = licenses.Count(l => l.Name != "None" && l.Name != "Other"),
    Licenses = licenses,
    LanguagesCount = languages.Count(),
    Languages = languages,
    TopRepositories = topRepositories,
    Years = years,
    LanguagesOverTheYears = languagesOverTheYears
});