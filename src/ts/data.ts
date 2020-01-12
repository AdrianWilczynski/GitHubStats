export interface Data {
    Total: Total;
    Voivodeships: Voivodeship[];
    Trivia: Trivia;
}

export interface Voivodeship {
    Name: string;
    DeveloperCount: number;
}

export interface Total {
    DeveloperCount: number;
    Population: number;
    Percentage: number;
}

export interface Trivia {
    BigCityWithFewestDevelopers: string;
    MediumCityWithFewestDevelopers: string;
    SmallestTownWithDeveloper: string;
    BigCityWithBestDeveloperToNonDeveloperRatio: string;
    MediumCityWithBestDeveloperToNonDeveloperRatio: string;
}

export interface CityData {
    RepositoryCount: number;
    LicensesCount: number;
    Licenses: License[];
    LanguagesCount: number;
    Languages: Language[];
}

export interface License {
    Count: number;
    Name: string;
}

export interface Language {
    Count: number;
    Name: string;
}