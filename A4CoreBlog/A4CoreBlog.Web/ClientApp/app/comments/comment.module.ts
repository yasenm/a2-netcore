import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { CommentsService } from "../shared/services/comments.service";
import { CommentListComponent } from "./comment-list.component";
import { AddCommentComponent } from "./add-comment.component";

import { SharedModule } from "../shared/shared.module";

@NgModule({
    declarations: [
        CommentListComponent,
        AddCommentComponent
    ],
    exports: [
        CommentListComponent,
        AddCommentComponent
    ],
    imports: [
        RouterModule,
        SharedModule
    ],
    providers: [
        CommentsService
    ]
})
export class CommentModule { }
