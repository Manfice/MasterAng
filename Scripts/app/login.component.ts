import { Component } from "@angular/core";
@Component({
    selector: "login",
    template: `
        <h1>{{title}}</h1>
        <div>
            <p>This is my login page</p>
        </div>
        `
})
export class LoginComponent {
    title = "Login page";
}