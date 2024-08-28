import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TopbarComponent } from './components/topbar/topbar.component';
import { NotificationComponent } from "./components/notification/notification.component";
import { AuthService } from './shared/services/auth/auth.service';
import { LoginDialogComponent } from './components/login-dialog/login-dialog.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TopbarComponent, NotificationComponent, LoginDialogComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  
  displayLoginDialog = false;
  
  private authService = inject(AuthService);

  ngOnInit(): void { 
    this.authService.triggerLoginObserver.subscribe(_=>{
      this.displayLoginDialog = true;
    });
  }
}
