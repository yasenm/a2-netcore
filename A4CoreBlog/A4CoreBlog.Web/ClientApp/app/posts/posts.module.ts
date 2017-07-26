import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { PostsComponent } from "./posts.component";
import { PostDetailsComponent } from "./post-details.component";
import { PostSampleListComponent } from "./post-sample-list.component";
import { PostsService } from "../shared/services/posts.service";
import { SharedModule } from "../shared/shared.module";

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'posts', component: PostsComponent },
            { path: 'post/:id', component: PostDetailsComponent },
        ])
    ],
    declarations: [
        PostsComponent,
        PostDetailsComponent,
        PostSampleListComponent
    ],
    exports: [
        PostsComponent,
        PostSampleListComponent
    ],
    providers: [
        PostsService
    ]
})
export class PostsModule {
}