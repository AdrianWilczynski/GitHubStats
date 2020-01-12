import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';

import 'chartjs-plugin-colorschemes';

import * as citiesGrid from './citiesGrid';
import * as voivodeshipPieChart from './voivodeshipsPieChart';
import * as totalSummary from './totalSummary';
import * as trivia from './trivia';
import * as languagesPieChart from './languagesPieChart';
import * as licensesPieChart from './licensesPieChart';
import * as opoleVsWroclawSummary from './opoleVsWroclawSummary';
import * as topRepositoriesGrid from './topRepositoriesGrid';
import * as repositoriesPerYearLineChart from './repositoriesPerYearLineChart';

import { Data, CityData } from './models';

Init();

async function Init() {
    citiesGrid.Init();

    const response = await fetch('data.json');
    const data = await response.json() as Data;

    const responseForOpole = await fetch('opole.json');
    const dataForOpole = await responseForOpole.json() as CityData;

    const responseForWroclaw = await fetch('wroclaw.json');
    const dataForWroclaw = await responseForWroclaw.json() as CityData;

    totalSummary.Init(data.Total);
    voivodeshipPieChart.Init(data.Voivodeships);
    trivia.Init(data.Trivia);

    opoleVsWroclawSummary.Init(dataForOpole, dataForWroclaw);

    languagesPieChart.Init('opole', dataForOpole.Languages);
    languagesPieChart.Init('wroclaw', dataForWroclaw.Languages);

    licensesPieChart.Init('opole', dataForOpole.Licenses);
    licensesPieChart.Init('wroclaw', dataForWroclaw.Licenses);

    topRepositoriesGrid.Init('opole', dataForOpole.TopRepositories);
    topRepositoriesGrid.Init('wroclaw', dataForWroclaw.TopRepositories);

    repositoriesPerYearLineChart.Init(dataForOpole.Years, dataForWroclaw.Years);
}
