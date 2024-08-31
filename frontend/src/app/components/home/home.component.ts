import { AfterViewInit, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth/auth.service';
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";
import { NotificationService } from '../../shared/services/notification/notification.service';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CreateVaultDialogComponent } from '../create-vault-dialog/create-vault-dialog.component';
import { VaultService } from '../../shared/services/vaults/vault.service';
import { Vault } from '../../shared/models/vault.model';
import { VaultDialogComponent } from '../vault-dialog/vault-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    LoginDialogComponent,
    CreateVaultDialogComponent,
    VaultDialogComponent,
    CommonModule, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit,OnDestroy {
  displayCreateVaultDialog = false;
  displayVaultDialog = false;

  private authService = inject(AuthService);
  private vaultService = inject(VaultService);
  private notificationService = inject(NotificationService);

  vaults:Vault[] = [];
  vault?:Vault;
  
  subscribeToUserLogin?:Subscription;
  
  ngOnDestroy(): void {
    this.subscribeToUserLogin?.unsubscribe();
  }

  ngOnInit(): void {
    this.setupVaults();
    this.setupSubs();
  }

  private setupSubs() {
    this.subscribeToUserLogin = this.authService.onUserLoggedIn.subscribe(()=>{
      this.setupVaults();
    });
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

  openVaultDialog(vaultId: string){
    this.vaultService.getVault(vaultId, {vaultId}).subscribe((v)=>{
      this.vault = {...v};
      this.displayVaultDialog = true;
    });
  }

  updateVault(vaultId:string) {
    this.vaultService.getVault(vaultId, {vaultId}).subscribe((v)=>{
      if(this.vault?.vaultId == vaultId){
        this.vault = {...v};
      }
    });

    this.setupVaults(); //refresh all vault definations
  }
}
