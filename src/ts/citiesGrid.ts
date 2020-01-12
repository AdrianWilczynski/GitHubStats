import { Grid, ColDef, GridOptions } from 'ag-grid-community';

interface City {
    Number?: number,
    Population: number
}

export async function Init() {
    const columns: ColDef[] = [
        { headerName: '#', field: 'Number', width: 60, sortable: false, filter: false },
        { headerName: 'Name', field: 'Name' },
        { headerName: 'Voivodeship', field: 'Voivodeship' },
        { headerName: 'Population', field: 'Population', filter: 'number' },
        { headerName: 'Developer Count', field: 'DeveloperCount', filter: 'number', cellClass: 'text-primary' },
        { headerName: 'Percentage', field: 'Percentage', filter: 'number' }
    ];

    const response = await fetch('cities.json');
    const cities = await response.json() as City[];

    const numberedCities = cities
        .map<City>((c, i) => {
            return {
                Number: i + 1,
                ...c
            }
        });

    const rows = numberedCities

    var gridOptions: GridOptions = {
        columnDefs: columns,
        rowData: rows,
        defaultColDef: {
            sortable: true,
            filter: true
        },
        onGridReady: e => e.api.sizeColumnsToFit()
    };

    const citiesGrid = document.getElementById('citiesGrid') as HTMLElement;
    new Grid(citiesGrid, gridOptions);
}