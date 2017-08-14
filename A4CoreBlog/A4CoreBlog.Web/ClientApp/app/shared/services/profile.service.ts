import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { AuthService } from "./auth.service";
import { BaseService } from "./base.service";

@Injectable()
export class ProfileService extends BaseService {
    constructor(private _http: Http,
        private _authService: AuthService,
        @Inject('ORIGIN_URL') private originUrl: string) {
        super();
    }

    public getProfileOfCurrentUser(): any {
        return this._http.get(this.originUrl + '/api/users/profile',
            this._authService.getAuthTokenHeaders());
    }
}