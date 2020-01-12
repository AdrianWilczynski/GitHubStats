import Chart from 'chart.js';
import { Year } from './models';

export function Init(yearsForOpole: Year[], yearsForWroclaw: Year[]) {
    new Chart(`repositoriesPerYear`, {
        type: 'line',
        data: {
            labels: yearsForOpole.map(y => y.Value.toString()),
            datasets: [
                {
                    label: 'Opole',
                    data: yearsForOpole.map(y => y.RepositoriesCreatedCount)
                },
                {
                    label: 'Wrocław',
                    data: yearsForWroclaw.map(y => y.RepositoriesCreatedCount)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'office.Urban6'
                }
            }
        }
    })
}