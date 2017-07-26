import { Component } from '@angular/core';

import { PostsComponent } from "../../posts/posts.component";
import { BlogsComponent } from "../../blogs/blogs.component";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [PostsComponent, BlogsComponent]
})
export class HomeComponent {
}
