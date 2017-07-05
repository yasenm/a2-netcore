import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";

@Component({
    selector: 'posts',
    templateUrl: './posts.component.html'
})
export class PostsComponent {
    public posts: PostInterface[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/posts/all').subscribe(result => {
            this.posts = result.json() as PostInterface[];
        });
    }
}

interface PostInterface {
    summary: string;
    title: string;
    id: number;
    authorId: string;
    blogId: number;
}