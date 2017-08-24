import { Component, Input, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";

import { AuthService } from "../shared/services/auth.service";
import { AddComment } from "./add-comment";
import { CommentsService } from "../shared/services/comments.service";

@Component({
    selector: 'add-comment',
    templateUrl: './add-comment.component.html',
    styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
    @Input() type: string;
    @Input() id: number;

    public loggedIn: boolean;
    public loading: boolean = false;
    public model: AddComment = new AddComment();

    constructor(private _authService: AuthService,
    private _comService: CommentsService) { }
    
    ngOnInit(): void {
        this.loggedIn = this._authService.isLoggedIn();
        this.eventsSubribing();
    }

    postComment(commentForm: NgForm): void {
        this.loading = true;
        let commentModel = commentForm.value as AddComment;
        commentModel.forId = this.id;
        this._comService.addComment(this.type, commentModel)
            .subscribe(res => {
                console.log(res);
                this.loading = false;
            }, err => {
                console.log(err);
                this.loading = false;
            });
    }

    private eventsSubribing(): void {
        this._authService.logginStateChange.subscribe(
            value => {
                this.loggedIn = value;
            })
    }
}
