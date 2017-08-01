import { Injectable, Inject } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";

import { LoginForm } from "../../auth/login-form";
import { CookieService } from "./cookie.service";

import "rxjs/add/operator/map";
import { AuthResponse } from "../../auth/auth-response";

@Injectable()
export class AuthService {
    private userCookieName: string = 'currentUser';
    public token: string;

    constructor(
        private _http: Http,
        private _cookieService: CookieService,
        @Inject('LOGIN_TOKEN_URL') private _loginTokenUrl: string)
    { }

    public isLoggedIn(): boolean {
        this.token = this._cookieService.getCookie(this.userCookieName);
        if (this.token) {
            return true;
        }
        return false;
    }

    public login(loginForm: LoginForm): any {
        let loginString = JSON.stringify(loginForm);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        console.log(loginString);
        return this._http.post(this._loginTokenUrl, loginForm, options)
            .map(res => {
                let authResponse = res.json() as AuthResponse;
                let token = res.json() && res.json().token;
                if (authResponse && authResponse.token) {
                    // set token property
                    this.token = authResponse.token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    this._cookieService.setCookie(this.userCookieName,
                        authResponse.token,
                        new Date(authResponse.expiration).toUTCString());
                    
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
        this._cookieService.deleteCookie(this.userCookieName);
        //localStorage.removeItem('currentUser');
    }
}