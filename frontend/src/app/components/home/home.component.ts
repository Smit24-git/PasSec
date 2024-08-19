import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth/auth.service';
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LoginDialogComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  
  isLoggedIn = false;
  displayLoginDialog = false;
  
  private authService = inject(AuthService);
  
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
}
