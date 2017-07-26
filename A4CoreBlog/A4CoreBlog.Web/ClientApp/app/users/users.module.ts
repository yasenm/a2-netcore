import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { UsersSmListComponent } from "./users-sm-list.component";
import { UserDetailsComponent } from "./user-details.component";

import { SharedModule } from "../shared/shared.module";
import { UsersService } from "../shared/services/users.service";
import { PostsModule } from "../posts/posts.module";
import { BlogsModule } from "../blogs/blogs.module";

@NgModule({
    imports: [
        SharedModule,
        PostsModule,
        BlogsModule,
        RouterModule.forChild([
            { path: 'users/:username', component: UserDetailsComponent }
        ])
    ],
    declarations: [
        UsersSmListComponent,
        UserDetailsComponent
    ],
    exports: [
        UsersSmListComponent
    ],
    providers: [
        UsersService
    ]
})
export class UsersModule {
}