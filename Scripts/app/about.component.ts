import { Component } from "@angular/core";
@Component({
    selector: "about",
    template: `
        <h1>{{title}}</h1>
        <div>
            <p>This is my about page</p>
        </div>
        `
})
export class AboutComponent {
    title = "About page";
}