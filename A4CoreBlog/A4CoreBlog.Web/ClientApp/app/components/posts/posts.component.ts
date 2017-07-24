import { Component, Inject, OnInit } from '@angular/core';
import { Http } from "@angular/http";

import { PostInterface } from './post';

@Component({
    selector: 'posts',
    templateUrl: './posts.component.html',
    styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
    public posts: PostInterface[];
    
    constructor(private _http: Http, @Inject('ORIGIN_URL') private originUrl: string) {}

    ngOnInit(): void {
        this._http.get(this.originUrl + '/api/posts/all').subscribe(result => {
            this.posts = result.json() as PostInterface[];
        });
    }
}