import { Inject, Injectable } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs';

@Injectable()
export class PostsService {
    constructor(private _http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }
    
    public getPosts(userId: string): any {
        return this._http.get(this.originUrl + '/api/posts/all',
            new RequestOptions({
                params: {
                    authorId: userId
                    //page: page,
                    //size: size
                }
            }));
    }

    public getPostsCount(userId: string): any {
        return this._http.get(this.originUrl + '/api/posts/count',
            new RequestOptions({
                params: {
                    authorId: userId
                }
            }));
    }
}