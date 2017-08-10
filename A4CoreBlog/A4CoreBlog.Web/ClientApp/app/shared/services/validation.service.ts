import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { FormGroup, AbstractControl } from "@angular/forms";

import 'rxjs/add/operator/debounceTime';

@Injectable()
export class ValidationService extends BaseService {
    private validationMessages = {
        required: 'Field is required.',
        pattern: 'Please enter valid input.',
        minlength: 'Min length of ',
        maxlength: 'Max length of '
    };

    constructor() { super(); }
    
    checkAndSetIfHasError(component: any, form: FormGroup, controlName: string, debTime: number = 0,  errorsParams?: any): void {
        const control = form.get(controlName);

        // for nested formgroups
        const lastIndesOfDot = controlName.lastIndexOf('.')
        if (lastIndesOfDot) {
            controlName = controlName.substring(lastIndesOfDot + 1);
        }
        control.valueChanges.debounceTime(debTime)
            .subscribe(value =>
                this.setMessage(component, control, controlName, errorsParams));
    }

    setMessage(component: any, c: AbstractControl, controlName: string, errorsParams?: any): void {
        if ((c.touched || c.dirty) && c.errors) {
            Object.keys(component).map(key => {
                if (key === (controlName + 'ErrMessage')) {
                    let errorMessage = Object.keys(c.errors).map(key => {
                        let composedErrorMessage: string = '';
                        composedErrorMessage += this.validationMessages[key];

                        if (errorsParams && errorsParams[key]) {
                            composedErrorMessage += `${errorsParams[key]}.`;
                        }
                        return composedErrorMessage;
                    }).join('');

                    component[key] = errorMessage;
                }
            })
        } else {
            console.log(`${controlName} is fine valid...`);
            component[controlName + 'ErrMessage'] = '';
        }
    };
}