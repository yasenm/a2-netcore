import { Component, Inject, OnInit } from '@angular/core';
import { Http } from "@angular/http";
import { Router, ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'post',
    templateUrl: './post-details.component.html'
})
export class PostDetailsComponent implements OnInit {
    public post: PostDetailsInterface;

    constructor(private _http: Http,
        @Inject('ORIGIN_URL') private originUrl: string,
        private _router: Router,
        private route: ActivatedRoute) {
        //_http.get(originUrl + '/api/posts/all').subscribe(result => {
        //    this.post = result.json() as PostDetailsInterface;
        //});
    }

    ngOnInit(): void {
        //let getParams = new URLSearchParams();
        this.route.params.subscribe(params =>
            //getParams.set('postId', params['id']);
            this._http.get(`${this.originUrl}/api/posts/get?postId=${params["id"]}`)
                .subscribe(result => this.post = result.json() as PostDetailsInterface)
        );
    }
}

interface PostDetailsInterface {
    summary: string;
    description: string;
    title: string;
    id: number;
    authorId: string;
    blogId: number;
}