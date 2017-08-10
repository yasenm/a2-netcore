import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";

import { LoginFormComponent } from "./login-form.component";
import { RegisterFormComponent } from "./register-form.component";
import { SharedModule } from "../shared/shared.module";
import { ValidationService } from "../shared/services/validation.service";

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
    providers: [
        ValidationService
    ]
})
export class AuthModule {
}