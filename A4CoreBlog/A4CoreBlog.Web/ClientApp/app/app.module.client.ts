import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { sharedConfig } from './app.module.shared';

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        ...sharedConfig.imports
    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin }
        //{ provide: 'LOGIN_URL', useValue: location.origin + '/auth/login?returnurl=' },
        //{ provide: 'LOGIN_TOKEN_URL', useValue: location.origin + '/api/clientauth/token' },
        //{ provide: 'REGISTER_URL', useValue: location.origin + '/api/clientauth/register' }
    ]
})
export class AppModule { }
