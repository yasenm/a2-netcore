import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent }  from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';

import { BlogsModule }  from "./blogs/blogs.module";
import { UsersModule }  from "./users/users.module";
import { PostsModule }  from "./posts/posts.module";
import { SharedModule } from "./shared/shared.module";
import { AuthModule }   from "./auth/auth.module";

export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        HomeComponent
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        SharedModule,
        PostsModule,
        BlogsModule,
        UsersModule,
        AuthModule
    ]
};
