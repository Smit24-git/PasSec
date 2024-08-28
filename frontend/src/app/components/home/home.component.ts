import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth/auth.service';
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";
import { NotificationService } from '../../shared/services/notification/notification.service';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CreateVaultDialogComponent } from '../create-vault-dialog/create-vault-dialog.component';
import { VaultService } from '../../shared/services/vaults/vault.service';
import { Vault } from '../../shared/models/vault.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LoginDialogComponent, CreateVaultDialogComponent, CommonModule, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  
  isLoggedIn = false;
  loggedInUser = '';
  displayLoginDialog = false;
  displayCreateVaultDialog = false;

  private authService = inject(AuthService);
  private vaultService = inject(VaultService);
  private notificationService = inject(NotificationService);

  vaults:Vault[] = [];
  
  ngOnInit(): void {
    setTimeout(()=>{
      this.isLoggedIn = this.authService.isLoggedIn();
      if(!this.isLoggedIn){
        // open login user dialog
        this.openLoginUserDialog();
      } {
        this.setupVaults();
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

  openCreateVaultDialog(){
    this.displayCreateVaultDialog = true
  }


  /**
   * 
   */
  private setupVaults() {
    this.vaultService.listUserVaults()
    .subscribe((res)=>{
      this.vaults = [...res.vaults];

    });
  }

  onCreateVaultDisplayChanges(){
    this.setupVaults();
  }
}
