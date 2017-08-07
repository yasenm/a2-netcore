import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";

import { UserRegister } from "./register-form";
import { AuthService } from "../shared/services/auth.service";

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
    userForm: FormGroup;
    user: UserRegister = new UserRegister();
    emailMessage: string;
    errors: string[];
    loading: boolean = false;
    success: boolean = false;

    private validationMessages = {
        required: 'Please enter you email address.',
        pattern: 'Please enter a valid email address.'
    };

    constructor(private _auth: AuthService, private _formBulder: FormBuilder) { }

    ngOnInit(): void {
        this.userForm = this._formBulder.group({
            firstName: ['', [Validators.required, Validators.minLength(4)]],
            lastName: ['', [Validators.required, Validators.minLength(4)]],
            email: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+')]],
            passwordGroup: this._formBulder.group({
                password: ['', [Validators.required, Validators.minLength(6)]],
                passwordConfirm: ['', [Validators.required, Validators.minLength(6)]],
            }, { validator: passwordMatcher }),
            profession: null,
        });

        const emailControl = this.userForm.get('email');
        emailControl.valueChanges.subscribe(value =>
            this.setMessage(emailControl))
    }

    register(): void {
        this.loading = true;
        this.user = this.userForm.value as UserRegister;
        this.user.password = this.userForm.get('passwordGroup.password').value;
        this.user.passwordConfirm = this.userForm.get('passwordGroup.passwordConfirm').value;
        this.errors = null;

        this._auth.register(this.user)
            .subscribe(res => {
                this.success = true;
                this.loading = false;
            },
            err => {
                console.log(err);
                this.errors = err;
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

    setMessage(c: AbstractControl): void {
        this.emailMessage = '';
        if ((c.touched || c.dirty) && c.errors) {
            this.emailMessage = Object.keys(c.errors).map(key =>
                this.validationMessages[key]).join(' ');
        }
    };
}