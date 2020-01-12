import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';

import 'chartjs-plugin-colorschemes';

import * as citiesGrid from './citiesGrid';
import * as voivodeshipPieChart from './voivodeshipsPieChart';
import * as totalSummary from './totalSummary';
import * as trivia from './trivia';

import { Data } from './data';

Init();

async function Init() {
    citiesGrid.Init();

    const response = await fetch('data.json');
    const data = await response.json() as Data;

    totalSummary.Init(data.Total);
    voivodeshipPieChart.Init(data.Voivodeships);
    trivia.Init(data.Trivia);
}
