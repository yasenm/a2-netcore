import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { FooterMenuComponent } from "./components/footer-menu/footer-menu.component";
import { SimpleLoaderComponent } from "./shared/loaders/simple-loader.component";
//import { PaginationComponent } from "./components/shared/pagination/pagination.component";
import { HomeComponent } from './components/home/home.component';
import { BlogsComponent } from './components/blogs/blogs.component';
import { BlogSampleListComponent } from "./components/blogs/blog-sample-list.component";
import { BlogInfoComponent } from './components/blogs/blog-info.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostDetailsComponent } from "./components/posts/post-details.component";
import { PostSampleListComponent } from "./components/posts/post-sample-list.component";
import { UsersSmListComponent } from "./components/users/users-sm-list.component";
import { UserDetailsComponent } from "./components/users/user-details.component";

export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        FooterMenuComponent,
        SimpleLoaderComponent,
        //PaginationComponent,
        PostsComponent,
        PostDetailsComponent,
        PostSampleListComponent,
        BlogsComponent,
        BlogSampleListComponent,
        BlogInfoComponent,
        UsersSmListComponent,
        UserDetailsComponent,
        HomeComponent
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'posts', component: PostsComponent },
            { path: 'post/:id', component: PostDetailsComponent },
            { path: 'blogs', component: BlogsComponent },
            { path: 'users/:username', component: UserDetailsComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        NgxPaginationModule
    ]
};
