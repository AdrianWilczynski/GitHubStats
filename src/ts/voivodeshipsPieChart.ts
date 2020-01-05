import Chart from 'chart.js';

interface Voivodeship {
    Name: string;
    DeveloperCount: number;
}

export async function Init() {
    const response = await fetch('voivodeships.json');
    const voivodeships = await response.json() as Voivodeship[];

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