import { Inject, Injectable } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';

@Injectable()
export class BlogsService {
    constructor(private _http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

    public getBlog(userId: string): any {
        return this._http.get(this.originUrl + '/api/blogs/details', new RequestOptions({ params: { userId: userId } }));
    }

    public getBlogs(): any {
        return this._http.get(this.originUrl + '/api/blogs/all');
    }
}