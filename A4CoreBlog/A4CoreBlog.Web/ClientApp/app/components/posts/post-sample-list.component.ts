import { Component, Input, OnInit, OnChanges, SimpleChanges } from "@angular/core";

import { PostInterface } from "./post";
import { PostsService } from "../../services/posts.service";

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'post-sample-list',
    templateUrl: './post-sample-list.component.html',
    styleUrls: ['./post-sample-list.component.css'],
    providers: [PostsService]
})
export class PostSampleListComponent implements OnInit, OnChanges {
    @Input() userId: string;
    public posts: PostInterface[];

    constructor(private _postsService: PostsService) { }

    private GetOrUpdatePostList() {
        this._postsService.getPosts(this.userId)
            .subscribe((data) => this.posts = data.json() as PostInterface[]);
    }

    ngOnInit(): void {
        this.GetOrUpdatePostList();
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.GetOrUpdatePostList();
    }
}