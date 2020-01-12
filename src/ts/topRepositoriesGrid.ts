import { Grid, ColDef, GridOptions } from 'ag-grid-community';
import { Repository } from './models';

export async function Init(cityId: string, repositories: Repository[]) {
    const columns: ColDef[] = [
        { headerName: 'Name', field: 'Name' },
        {
            headerName: 'Description', field: 'Description', cellStyle: {
                'white-space': 'normal',
                'line-height': '1.5em'
            }
        },
        { headerName: 'Language', field: 'Language', width: 100 },
        { headerName: 'Stars', field: 'Stars', filter: 'number', width: 100 }
    ];

    var gridOptions: GridOptions = {
        columnDefs: columns,
        rowData: repositories,
        rowHeight: 100,
        defaultColDef: {
            sortable: true,
            filter: true
        },
        onGridReady: e => e.api.sizeColumnsToFit()
    };

    const grid = document.getElementById(`${cityId}TopRepositoriesGrid`) as HTMLElement;
    new Grid(grid, gridOptions);
}