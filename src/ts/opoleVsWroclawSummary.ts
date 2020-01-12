import { CityData } from "./data";

export function Init(opole: CityData, wroclaw: CityData) {
    document.getElementById('opoleRepositoryCount')!.innerText = opole.RepositoryCount.toString();
    document.getElementById('wroclawRepositoryCount')!.innerText = wroclaw.RepositoryCount.toString();

    document.getElementById('opoleLanguagesCount')!.innerText = opole.LanguagesCount.toString();
    document.getElementById('wroclawLanguagesCount')!.innerText = wroclaw.LanguagesCount.toString();

}