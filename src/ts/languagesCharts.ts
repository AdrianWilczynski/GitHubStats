import Chart from 'chart.js';
import { Language } from './models';

export function Init(languages: Language[]) {
    const top10 = languages.slice(0, 10);

    new Chart('issuesPerLanguage', {
        type: 'bar',
        data: {
            labels: top10.map(l => l.Name),
            datasets: [
                {
                    label: 'Issues Average',
                    data: top10.map(l => l.IssuesAverage)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'tableau.PurplePinkGray12'
                }
            }
        }
    });

    new Chart('starsPerLanguage', {
        type: 'bar',
        data: {
            labels: top10.map(l => l.Name),
            datasets: [
                {
                    label: 'Stars Average',
                    data: top10.map(l => l.StarsAverage)
                }
            ]
        },
        options: {
            responsive: false,
            plugins: {
                colorschemes: {
                    scheme: 'tableau.RedBlackWhite7'
                }
            }
        }
    });
}