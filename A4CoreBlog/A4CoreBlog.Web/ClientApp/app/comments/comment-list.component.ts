import { Component, Input, OnInit } from "@angular/core";
import { CommentsService } from "../shared/services/comments.service";
import { Comment } from "./comment";

@Component({
    selector: 'comment-list',
    templateUrl: './comment-list.component.html',
    styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
    @Input() type: string;
    @Input() id: number;

    public comments: Comment[];

    constructor(private _comService: CommentsService) { }

    ngOnInit(): void {
        this.getComments();
        this.subscribeToEvents();
    }

    getComments(): void {
        if (this.type === 'post') {
            this._comService.getPostComments(this.id)
                .subscribe(res => this.comments = res.json() as Comment[]);
        } else if (this.type === 'blog') {
            // add blog comments when able
            //this._comService.getPostComments(this.id)
            //    .subscribe(res => this.comments = res.json() as Comment[]);
        }
    }

    private subscribeToEvents(): void {
        this._comService.commentsMustUpdate.subscribe(
            value => {
                if (value) {
                    this.getComments();
                }
            })
    }
}