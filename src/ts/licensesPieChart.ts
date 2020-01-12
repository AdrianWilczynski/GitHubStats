import Chart from 'chart.js';
import { License } from './data';

export function Init(cityId: string, licenses: License[]) {
    new Chart(`${cityId}LicensesPieChart`, {
        type: 'pie',
        data: {
            labels: licenses.map(v => v.Name),
            datasets: [
                {
                    data: licenses.map(v => v.Count)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'brewer.PuRd7'
                }
            }
        }
    })
}