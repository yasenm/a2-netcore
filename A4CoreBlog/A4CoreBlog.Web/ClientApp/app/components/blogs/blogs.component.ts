import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

import { BlogInterface } from './blog';

@Component({
    selector: 'blogs',
    templateUrl: './blogs.component.html'
})
export class BlogsComponent {
    public blogs: BlogInterface[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/blogs/all').subscribe(result => {
            this.blogs = result.json() as BlogInterface[];
        });
    }
}
