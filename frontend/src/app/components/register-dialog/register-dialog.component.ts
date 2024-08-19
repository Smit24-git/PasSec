import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordPattern } from '../../shared/regular-expressions/password-pattern';
import { passwordMismatchValidator } from '../../shared/custom-validators/password-match-validator';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../shared/services/auth/auth.service';
import { NotificationService } from '../../shared/services/notification/notification.service';

@Component({
  selector: 'app-register-dialog',
  standalone: true,
  imports: [SharedModule, CommonModule],
  templateUrl: './register-dialog.component.html',
  styleUrl: './register-dialog.component.scss'
})
export class RegisterDialogComponent implements OnInit{
  
  @Input({required:true}) display = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() onRegistered = new EventEmitter<{username:string}>();

  registerForm!:FormGroup;

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private notificationService = inject(NotificationService);

  ngOnInit(): void {
    this.buildRegisterForm();
  }

  private buildRegisterForm() {
    this.registerForm = this.fb.group({
      username: [,[Validators.required, Validators.minLength(3)]],
      password: [, [Validators.required, Validators.pattern(PasswordPattern)]],
      retypePassword: [, [Validators.required] ],
    }, {
      validators: passwordMismatchValidator
    });
  }

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  register(){
    if(this.registerForm.invalid) return;

    var registerFormValues = this.registerForm.getRawValue();
    this.authService.registerUser({
      userName: registerFormValues.username,
      password: registerFormValues.password
    }).subscribe(()=>{
      this.notificationService.showMessage({severity:'success',summary: 'Registered successfully!'});
      this.onRegistered.emit({username: registerFormValues.username});
      this.closeDialog();
    });
  }
}
