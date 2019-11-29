using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

public static string GetDataFilePath(string fileName) => Path.Join(GetScriptFolder(), "..", "data", fileName);