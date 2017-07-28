import { Inject, Injectable } from '@angular/core';
import { CanActivate, Router } from "@angular/router";

import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private _auth: AuthService,
        private _router: Router,
        @Inject('LOGIN_URL') private loginUrl: string) { }

    canActivate(): boolean {
        if (!this._auth.isLoggedIn()) {
            this._router.navigate(['/login']);
            return false;
        }
        
        return true;
    }
}