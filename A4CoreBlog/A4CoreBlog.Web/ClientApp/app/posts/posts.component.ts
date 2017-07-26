import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';

import { PostInterface } from './post';
import { PostsService } from "../shared/services/posts.service";

@Component({
    selector: 'posts',
    templateUrl: './posts.component.html',
    styleUrls: ['./posts.component.css'],
    providers: [PostsService]
})
export class PostsComponent implements OnInit, OnChanges {
    public p: number = 1;
    public size: number = 3;
    public total: number;
    public posts: PostInterface[];

    constructor(private _postsService: PostsService) { }

    getPosts() {
        this._postsService.getPosts('')
            .subscribe((data) => this.posts = data.json() as PostInterface[]);
    }

    ngOnInit(): void {
        this.getPosts();
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.getPosts();
    }
}