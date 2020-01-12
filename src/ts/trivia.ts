import { Trivia } from "./models";

export function Init(trivia: Trivia) {
    for (const key of Object.keys(trivia)) {
        const id = key.charAt(0).toLowerCase() + key.substring(1);

        document.getElementById(id)!.innerText = trivia[key as keyof Trivia];
    }
}