import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';

import 'chartjs-plugin-colorschemes';

import * as citiesGrid from './citiesGrid';
import * as voivodeshipPieChart from './voivodeshipsPieChart';

citiesGrid.Init();
voivodeshipPieChart.Init();