import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth/auth.service';
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";
import { NotificationService } from '../../shared/services/notification/notification.service';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LoginDialogComponent, CommonModule, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  
  isLoggedIn = false;
  loggedInUser = '';
  displayLoginDialog = false;
  
  private authService = inject(AuthService);
  private notificationService = inject(NotificationService);
  
  ngOnInit(): void {
    setTimeout(()=>{
      this.isLoggedIn = this.authService.isLoggedIn();
      if(!this.isLoggedIn){
        // open login user dialog
        this.openLoginUserDialog();
      }
    }, 200);
  }
  private openLoginUserDialog(){
    this.displayLoginDialog = true;
  }

  onUserLoggedIn(){
    this.isLoggedIn = this.authService.isLoggedIn();
    if(this.isLoggedIn) {
      this.loggedInUser = this.authService.getLoggedInUserName() ?? '';
    } else {
      this.notificationService.showMessage({severity: 'error', summary: 'something went wrong. please try again.'});
      this.openLoginUserDialog();      
    }
  }
}
