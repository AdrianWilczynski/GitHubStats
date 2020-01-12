import Chart from 'chart.js';
import { LanguageOverTheYears } from './models';

export function Init(cityId: string, languages: LanguageOverTheYears[]) {
    new Chart(`${cityId}LanguagesOverTheYearsLineChart`, {
        type: 'line',
        data: {
            labels: languages[0].Years.map(y => y.Value.toString()),
            datasets: languages.map<Chart.ChartDataSets>(l => {
                return {
                    label: l.Name,
                    data: l.Years.map(y => y.RepositoriesCreatedCount)
                };
            })
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'office.Atlas6'
                }
            }
        }
    })
}