import {Component} from "@angular/core";
import {FormBuilder, FormGroup, FormControl, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
    selector: "login",
    template: `
        <div class="login-container">
            <h2 class="form-login-heading">Login</h2>
            <div class="alert alert-danger" role="alert" *ngIf="loginError">
                <strong>Warning:</strong> Username or Password mismatch
            </div>
            <form class="form-login" [formGroup]="loginForm" (ngSubmit)="performLogin($event)">
                <input [formControl]="username" type="text" class="form-control" placeholder="Your username or e-mail address" required autofocus />
                <input [formControl]="password" type="password" class="form-control" placeholder="Your password" required />
                <div class="checkbox">
                    <label>
                        <input type="checkbox" value="remember-me"> Remember Me
                    </label>
                </div>
                <button class="btn btn-lg btn-primary btn-block" typpe="submit">Sign in</button>
            </form>
        </div>
    `
})

export class LoginComponent {
    title = "Login";
    loginForm = null;

    constructor(private fb: FormBuilder, private router: Router) {
        this.loginForm = fb.group({
            username: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    performLogin(e) {
        e.preventDefault();
        alert(JSON.stringify(this.loginForm.value));
    }
}