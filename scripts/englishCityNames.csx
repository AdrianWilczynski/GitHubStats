private var HardcodedNames = new Dictionary<string, string>
{
    { "Warszawa", "Warsaw" },
    { "Kraków", "Cracow" }
};

private string RemovePolishCharacters(string name)
    => name.Replace("ą", "a")
        .Replace("Ą", "A")
        .Replace("ć", "c")
        .Replace("Ć", "C")
        .Replace("ę", "e")
        .Replace("Ę", "E")
        .Replace("ł", "l")
        .Replace("Ł", "L")
        .Replace("ń", "n")
        .Replace("Ń", "N")
        .Replace("ó", "o")
        .Replace("Ó", "O")
        .Replace("ś", "s")
        .Replace("Ś", "S")
        .Replace("ź", "z")
        .Replace("Ź", "Z")
        .Replace("ż", "z")
        .Replace("Ż", "Z");

public IEnumerable<string> GetEnglishNames(string polishCityName)
{
    if (HardcodedNames.TryGetValue(polishCityName, out var englishName))
    {
        yield return englishName;
    }

    yield return RemovePolishCharacters(polishCityName);
}