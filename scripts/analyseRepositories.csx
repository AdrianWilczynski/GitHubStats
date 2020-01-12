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

private var repositories = LoadDataFile<Dictionary<string, IEnumerable<RepositoryFromApi>>>(input);

private var licenses = repositories
    .SelectMany(u => u.Value)
    .GroupBy(r => r.license?.name ?? "None")
    .Select(g => new License
    {
        Name = g.Key,
        Count = g.Count()
    });

SaveDataFile(output, new CityData
{
    RepositoryCount = repositories.Sum(u => u.Value.Count()),
    LicensesCount = licenses.Count(l => l.Name != "None" && l.Name != "Other"),
    Licenses = licenses,
});