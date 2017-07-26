import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { BlogsComponent } from "./blogs.component";
import { BlogSampleListComponent } from "./blog-sample-list.component";
import { BlogInfoComponent } from "./blog-info.component";

import { SharedModule } from "../shared/shared.module";

@NgModule({
    declarations: [
        BlogsComponent,
        BlogSampleListComponent,
        BlogInfoComponent
    ],
    exports: [
        BlogsComponent,
        BlogSampleListComponent,
        BlogInfoComponent
    ],
    imports: [
        SharedModule,
        RouterModule.forRoot([
            { path: 'blogs', component: BlogsComponent }
        ])
    ]
})
export class BlogsModule {
}