import Chart from 'chart.js';
import { Language } from './data';

export function Init(cityId: string, languages: Language[]) {
    new Chart(`${cityId}LanguagesPieChart`, {
        type: 'pie',
        data: {
            labels: languages.map(l => l.Name),
            datasets: [
                {
                    data: languages.map(l => l.Count)
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