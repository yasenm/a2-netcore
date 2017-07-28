import { Injectable, Inject } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";

import { LoginForm } from "../../auth/login";

import "rxjs/add/operator/map";

@Injectable()
export class AuthService {
    private loggedIn: boolean = false;
    public token: string;

    constructor(
        private _http: Http,
        @Inject('LOGIN_TOKEN_URL') private _loginTokenUrl: string) {
        //console.log(!!this.getCookie('.AspNetCore.Identity.Application'));
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        this.loggedIn = !!localStorage.getItem('currentUser');
        //this.loggedIn = !!this.getCookie('.AspNetCore.Identity.Application');
    }

    public isLoggedIn(): boolean {
        return this.loggedIn;
    }

    public login(loginForm: LoginForm): any {
        let loginString = JSON.stringify(loginForm);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        console.log(loginString);
        return this._http.post(this._loginTokenUrl, loginForm, options)
            .map(res => {
                let token = res.json() && res.json().token;
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: loginForm.username, token: token }));
                    this.loggedIn = true;
                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    public logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }
}