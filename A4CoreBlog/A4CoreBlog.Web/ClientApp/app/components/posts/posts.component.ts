import { Component, OnInit } from '@angular/core';

import { PostInterface } from './post';
import { PostsService } from "../../shared/services/posts.service";

@Component({
    selector: 'posts',
    templateUrl: './posts.component.html',
    styleUrls: ['./posts.component.css'],
    providers: [PostsService]
})
export class PostsComponent implements OnInit {
    public p: number = 1;
    public posts: PostInterface[];

    constructor(private _postsService: PostsService) { }

    ngOnInit(): void {
        this._postsService.getPosts('')
            .subscribe((data) => this.posts = data.json() as PostInterface[]);
    }
}