import { Component } from "@angular/core";
import { AuthService } from "../shared/services/auth.service";
import { LoginForm } from "./login";
import { Router } from "@angular/router";

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

    public login(loginModel: LoginForm) {
        this.loading = true;
        this._auth.login(loginModel)
            .subscribe(result => {
                if (result) {
                    this._router.navigate(['/home']);
                } else {
                    this.error = 'Username or password is incorrect';
                    this.loading = false;
                }
            });
    }
}