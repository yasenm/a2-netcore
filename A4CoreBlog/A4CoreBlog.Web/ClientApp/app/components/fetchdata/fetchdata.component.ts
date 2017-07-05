import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public blogs: BlogInterface[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/blogs/all').subscribe(result => {
            this.blogs = result.json() as BlogInterface[];
        });
    }
}

interface BlogInterface {
    description: string;
    summary: string;
    title: string;
    id: number;
}
