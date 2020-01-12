import Chart from 'chart.js';
import { License } from './models';

export function Init(cityId: string, licenses: License[]) {
    new Chart(`${cityId}LicensesPieChart`, {
        type: 'pie',
        data: {
            labels: licenses.map(l => l.Name),
            datasets: [
                {
                    data: licenses.map(l => l.Count)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'tableau.ClassicTrafficLight9'
                }
            }
        }
    })
}