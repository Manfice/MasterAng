import { Component } from "@angular/core";
@Component({
    selector: "page-not-found",
    template: `
        <h1>{{title}}</h1>
        <div>
            <p>This page was not found there...</p>
        </div>
        `
})
export class PageNotFoundComponent {
    title = "404 page";
}