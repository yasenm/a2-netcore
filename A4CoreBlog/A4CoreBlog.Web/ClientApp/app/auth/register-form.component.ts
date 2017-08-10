import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";

import { UserRegister } from "./register-form";
import { AuthService } from "../shared/services/auth.service";
import { ValidationService } from "../shared/services/validation.service";

function passwordMatcher(c: AbstractControl) {
    let password = c.get('password');
    let passwordConfirm = c.get('passwordConfirm');
    if ((password.value === passwordConfirm.value) ||
        (password.pristine || passwordConfirm.pristine)) {
        return null;
    }
    return { 'match': true };
}

@Component({
    selector: 'register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
    debounceTimeForForm: number = 1000;
    userForm: FormGroup;
    user: UserRegister = new UserRegister();
    firstNameErrMessage: string = '';
    lastNameErrMessage: string = '';
    emailErrMessage: string = '';
    passwordErrMessage: string = '';
    passwordConfirmErrMessage: string = '';
    serverErrors: string[];
    loading: boolean = false;
    success: boolean = false;

    constructor(private _auth: AuthService,
        private _formBulder: FormBuilder,
        private _valService: ValidationService) { }

    ngOnInit(): void {
        this.userForm = this._formBulder.group({
            firstName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            lastName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            email: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+')]],
            passwordGroup: this._formBulder.group({
                password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(50)]],
                passwordConfirm: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(50)]],
            }, { validator: passwordMatcher }),
            profession: null,
        });

        this._valService.checkAndSetIfHasError(this, this.userForm, 'firstName', this.debounceTimeForForm, { minlength: 4, maxlength: 50 });
        this._valService.checkAndSetIfHasError(this, this.userForm, 'lastName', this.debounceTimeForForm, { minlength: 4, maxlength: 50 });
        this._valService.checkAndSetIfHasError(this, this.userForm, 'email');
        this._valService.checkAndSetIfHasError(this, this.userForm, 'passwordGroup.password', this.debounceTimeForForm, { minlength: 6});
        this._valService.checkAndSetIfHasError(this, this.userForm, 'passwordGroup.passwordConfirm', this.debounceTimeForForm, { minlength: 6 });
    }

    register(): void {
        this.loading = true;
        this.user = this.userForm.value as UserRegister;
        this.user.password = this.userForm.get('passwordGroup.password').value;
        this.user.passwordConfirm = this.userForm.get('passwordGroup.passwordConfirm').value;
        this.serverErrors = null;

        this._auth.register(this.user)
            .subscribe(res => {
                this.success = true;
                this.loading = false;
            },
            err => {
                console.log(err);
                this.serverErrors = err;
                this.loading = false;
            });
    }

    populateTestData(): void {
        this.userForm.patchValue({
            firstName: 'Yasen',
            lastName: 'Mihaylov',
            email: 'yasen8707@abv.bg',
        })
    }
}