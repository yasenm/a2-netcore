import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";

import { LoginFormComponent } from "./login-form.component";
import { RegisterFormComponent } from "./register-form.component";
import { AuthService } from "../shared/services/auth.service";
import { SharedModule } from "../shared/shared.module";

@NgModule({
    imports: [
        SharedModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: 'login', component: LoginFormComponent },
            { path: 'register', component: RegisterFormComponent }
        ])
    ],
    declarations: [
        LoginFormComponent,
        RegisterFormComponent
    ],
    exports: [
    ],
    providers: [
        AuthService
    ]
})
export class AuthModule {
}