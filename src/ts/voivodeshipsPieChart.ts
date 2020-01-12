import Chart from 'chart.js';
import { Voivodeship } from './models';

export function Init(voivodeships: Voivodeship[]) {
    new Chart('voivodeshipsPieChart', {
        type: 'pie',
        data: {
            labels: voivodeships.map(v => v.Name),
            datasets: [
                {
                    data: voivodeships.map(v => v.DeveloperCount)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'tableau.JewelBright9'
                }
            }
        }
    })
}