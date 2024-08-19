import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordMismatchValidator:ValidatorFn = (control:AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const retypePassword = control.get('retypePassword');

    if(password && retypePassword && (password.value || retypePassword.value) && password.value != retypePassword.value)
        return {mismatchPassword: true}

    return null
}