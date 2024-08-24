import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { RegisterDialogComponent } from '../register-dialog/register-dialog.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordPattern } from '../../shared/regular-expressions/password-pattern';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../shared/services/auth/auth.service';
import { NotificationService } from '../../shared/services/notification/notification.service';

@Component({
  selector: 'app-login-dialog',
  standalone: true,
  imports: [SharedModule, RegisterDialogComponent, CommonModule],
  templateUrl: './login-dialog.component.html',
  styleUrl: './login-dialog.component.scss'
})
export class LoginDialogComponent implements OnInit {

  @Input({required:true}) display:boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() onLoggedIn = new EventEmitter<void>();

  displayRegisterDialog = false;

  loginForm!:FormGroup;

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private notification = inject(NotificationService);

  ngOnInit(): void {
    this.buildLoginForm();
  }

  private buildLoginForm() {
    this.loginForm = this.fb.group({
      username: [, [Validators.required, Validators.minLength(3)]],
      password: [, [Validators.required, Validators.pattern(PasswordPattern)]],
    });
  }

  closeDialog() {
    this.displayChange.emit(this.display = false);
  }

  openRegisterDialog() {
    this.displayRegisterDialog = true;
  }
  
  login(){
    if(this.loginForm.invalid) return;

    var loginFormValues = this.loginForm.getRawValue();
    this.authService.loginUser({
      userName: loginFormValues.username,
      password: loginFormValues.password,
    }).subscribe(()=>{
      this.notification.showMessage({severity: 'success', summary: 'Logged in successfully!'});
      this.onLoggedIn.emit();
      this.closeDialog();
    });
  }

  onUserRegistered({username}: {username:string}) {
    this.loginForm.get('username')?.setValue(username);
  }
}
