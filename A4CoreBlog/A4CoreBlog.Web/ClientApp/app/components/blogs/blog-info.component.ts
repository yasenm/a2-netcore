import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';

import { BlogInterface } from './blog';
import { BlogsService } from '../../services/blogs.service';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'blog-info',
    templateUrl: './blog-info.component.html',
    providers: [BlogsService]
})
export class BlogInfoComponent implements OnInit, OnChanges {
    @Input() userId: string;

    public blog: BlogInterface;

    constructor(private _blogsService: BlogsService) { }

    private GetOrUpdateBlog() {
        this._blogsService.getBlog(this.userId)
            .subscribe((data) => this.blog = data.json() as BlogInterface);
    }

    ngOnInit(): void {
        this.GetOrUpdateBlog();
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.GetOrUpdateBlog();
    }
}