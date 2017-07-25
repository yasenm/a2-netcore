import { Inject, Injectable } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs';

@Injectable()
export class UsersService {
    constructor(private _http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

    public getUser(username: string): any {
        return this._http.get(this.originUrl + '/api/users/details', new RequestOptions({ params: { username: username } }));
    }

    public getUsers(): any {
        return this._http.get(this.originUrl + '/api/users/all');
    }
}