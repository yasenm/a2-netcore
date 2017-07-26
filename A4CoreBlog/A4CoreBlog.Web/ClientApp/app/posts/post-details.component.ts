import { Component, Inject, OnInit } from '@angular/core';
import { Http } from "@angular/http";
import { Router, ActivatedRoute } from "@angular/router";

import { PostInterface } from "./post";

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'post',
    templateUrl: './post-details.component.html'
})
export class PostDetailsComponent implements OnInit {
    public post: PostInterface;

    constructor(private _http: Http,
        @Inject('ORIGIN_URL') private originUrl: string,
        private _router: Router,
        private route: ActivatedRoute)
    {
    }

    ngOnInit(): void {
        this.route.params.subscribe(params =>
            this._http.get(`${this.originUrl}/api/posts/get?postId=${params["id"]}`)
                .subscribe(result => this.post = result.json() as PostInterface)
        );
    }
}