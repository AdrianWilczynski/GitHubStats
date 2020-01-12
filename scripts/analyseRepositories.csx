#pragma warning disable IDE1006, RCS1110

#load "shared.csx"
#load "models.csx"

using System.Linq;

private class RepositoryFromApi
{
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
        Count = g.Count()
    })
    .OrderByDescending(l => l.Count);

SaveDataFile(output, new CityData
{
    RepositoryCount = repositoriesPerUser.Sum(u => u.Value.Count()),
    LicensesCount = licenses.Count(l => l.Name != "None" && l.Name != "Other"),
    Licenses = licenses,
    LanguagesCount = languages.Count(),
    Languages = languages
});