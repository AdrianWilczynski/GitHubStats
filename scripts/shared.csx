#r "nuget: Newtonsoft.Json, 12.0.3"

using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null)
    => Path.GetDirectoryName(path);

public static string GetDataFilePath(string name)
    => Path.Join(GetScriptFolder(), "..", "data", name + ".json");

public static void SaveDataFile<T>(string name, T content)
    => File.WriteAllText(GetDataFilePath(name), JsonConvert.SerializeObject(content, Formatting.Indented));

public static T LoadDataFile<T>(string name)
    => JsonConvert.DeserializeObject<T>(File.ReadAllText(GetDataFilePath(name)));

public static bool HasNextPage(HttpResponseMessage response)
    => response.Headers.TryGetValues("Link", out var values)
    && values.Any(v => v.Contains("rel=\"next\""));