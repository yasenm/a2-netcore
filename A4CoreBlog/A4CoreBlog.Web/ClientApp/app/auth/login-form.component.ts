﻿import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";

import { AuthService } from "../shared/services/auth.service";
import { LoginForm } from "./login-form";

@Component({
    selector: 'login-form',
    templateUrl: './login-form.component.html',
    providers: [AuthService]
})
export class LoginFormComponent {
    public model: LoginForm = new LoginForm();
    public loading: boolean = false;
    public error: string;

    constructor(private _auth: AuthService,
        private _router: Router)
    { }

    public login(loginForm: NgForm) {
        this.loading = true;
        let loginModel = loginForm.value as LoginForm;
        this._auth.login(loginModel)
            .subscribe(result => {
                if (result) {
                    this._router.navigate(['/home']);
                } else {
                    this.error = 'Username or password is incorrect';
                }
                this.loading = false;
            });
    }
}