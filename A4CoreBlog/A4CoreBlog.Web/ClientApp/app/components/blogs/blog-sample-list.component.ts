import { Component, OnInit } from "@angular/core";

import { BlogsService } from "../../services/blogs.service";
import { BlogInterface } from "./blog";

@Component({
    selector: 'blog-sample-list',
    templateUrl: './blog-sample-list.component.html',
    providers: [BlogsService]
})
export class BlogSampleListComponent implements OnInit {
    public blogs: BlogInterface[];

    constructor(private _blogsService: BlogsService) { }

    ngOnInit(): void {
        this._blogsService.getBlogs()
            .subscribe(result => {
                this.blogs = result.json() as BlogInterface[];
            });
    }
}