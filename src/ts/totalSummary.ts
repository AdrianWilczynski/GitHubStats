import { Total } from "./models";

export function Init(total: Total) {
    document.getElementById('totalSummaryDeveloperCount')!.innerText = total.DeveloperCount.toString();
    document.getElementById('totalSummaryPercentage')!.innerText = total.Percentage.toString();
}