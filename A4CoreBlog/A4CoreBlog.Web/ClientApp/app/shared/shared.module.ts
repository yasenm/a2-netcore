﻿import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { BrowserModule } from "@angular/platform-browser";

import { SimpleLoaderComponent } from "./loaders/simple-loader.component";
import { NavMenuComponent } from './navmenu/navmenu.component';
import { FooterMenuComponent } from "./footer-menu/footer-menu.component";
import { NgxPaginationModule } from "ngx-pagination/dist/ngx-pagination";
import { RouterModule } from "@angular/router";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        BrowserModule,
        NgxPaginationModule,
        RouterModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        BrowserModule,
        NgxPaginationModule,
        SimpleLoaderComponent,
        NavMenuComponent,
        FooterMenuComponent
    ],
    declarations: [
        SimpleLoaderComponent,
        NavMenuComponent,
        FooterMenuComponent
    ],
})
export class SharedModule {
}