import { Component, Input, OnInit, ViewContainerRef } from "@angular/core";
import { NgForm } from "@angular/forms";

import { AuthService } from "../shared/services/auth.service";
import { AddComment } from "./add-comment";
import { CommentsService } from "../shared/services/comments.service";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

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
        private _comService: CommentsService,
        private toastr: ToastsManager,
        private vcr: ViewContainerRef) {
        this.toastr.setRootViewContainerRef(vcr);
    }

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
                this.toastr.success("Thank you for the opignion!");
                this.model = new AddComment();
            }, err => {
                console.log(err);
                this.loading = false;
                this.toastr.error("Something went wrong!");
            });
    }

    private eventsSubribing(): void {
        this._authService.logginStateChange.subscribe(
            value => {
                this.loggedIn = value;
            })
    }
}
