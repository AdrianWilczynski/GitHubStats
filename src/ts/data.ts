export interface Data {
    Total: Total;
    Voivodeships: Voivodeship[];
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