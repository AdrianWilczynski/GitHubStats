import { Grid, ColDef, GridOptions } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';

interface City {
    Number?: number,
    Population: number
}

async function Init() {
    const columns: ColDef[] = [
        { headerName: '#', field: 'Number', width: 60, sortable: false, filter: false },
        { headerName: 'Name', field: 'Name' },
        { headerName: 'Voivodeship', field: 'Voivodeship' },
        { headerName: 'Population', field: 'Population' },
        { headerName: 'Developer Count', field: 'DeveloperCount' },
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
        enableSorting: true,
        enableFilter: true,
        onGridReady: e => e.api.sizeColumnsToFit()
    };

    const citiesGrid = document.getElementById('citiesGrid') as HTMLElement;
    new Grid(citiesGrid, gridOptions);
}

Init();