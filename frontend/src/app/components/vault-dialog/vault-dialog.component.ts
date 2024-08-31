import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Vault, VaultStorageKey } from '../../shared/models/vault.model';
import { PasswordMaskPipe } from '../../shared/pipes/passwordMask/password-mask.pipe';
import {Clipboard, ClipboardModule} from '@angular/cdk/clipboard';
import { AddVaultSecurityKeyDialogComponent } from '../add-vault-security-key-dialog/add-vault-security-key-dialog.component';
import { VaultService } from '../../shared/services/vaults/vault.service';
import { NotificationService } from '../../shared/services/notification/notification.service';
import { StorageKeyDialogComponent } from '../storage-key-dialog/storage-key-dialog.component';

@Component({
  selector: 'app-vault-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SharedModule,
    PasswordMaskPipe,
    ClipboardModule,
    AddVaultSecurityKeyDialogComponent,
    StorageKeyDialogComponent,
  ],
  templateUrl: './vault-dialog.component.html',
  styleUrl: './vault-dialog.component.scss'
})
export class VaultDialogComponent {
  @Input({required:true}) display = false;
  @Input({required:true}) vault!:Vault;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() onVaultUpdated = new EventEmitter();


  private clipboard = inject(Clipboard);
  private vaultService = inject(VaultService);
  private notificationService = inject(NotificationService);

  displayAddVaultStorageKeyDialog = false;

  isUserEditingVName = false;
  isUserEditingVDesc = false;
  newVaultName?:string | null;
  newDescription?:string | null;

  openedStorageKey?:VaultStorageKey | null;
  displayStorageKeyDialog = false;

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  openStorageKeyDialog(storageKey:VaultStorageKey){
    this.openedStorageKey = {...storageKey};
    this.displayStorageKeyDialog = true;
  }

  openAddKeyDialog(){
    this.displayAddVaultStorageKeyDialog = true;
  }

  onKeyAdded() {
    this.onVaultUpdated.emit();
  }

  enableVaultEditor(){
    this.newVaultName = this.vault.vaultName;
    this.isUserEditingVName = true;
  }
  enableDescriptionEditor(){
    this.newDescription = this.vault.description;
    this.isUserEditingVDesc = true;
  }

  updateVaultName(){
    this.isUserEditingVName = false;
    this.vault.vaultName = this.newVaultName!;
    // save vault
    this.vaultService.updateVault(this.vault.vaultId, this.vault)
    .subscribe((res)=>{
      this.onVaultUpdated.emit();
      this.notificationService.showMessage({severity: 'success', summary: 'Vault Updated!'})
    });
  }
  
  updateVaultDescription(){
    this.isUserEditingVDesc = false;
    this.vault.description = this.newDescription!;
    // save vault
    this.vaultService.updateVault(this.vault.vaultId, this.vault)
    .subscribe((res)=>{
      this.onVaultUpdated.emit();
      this.notificationService.showMessage({severity: 'success', summary: 'Vault Updated!'})
    });
  }

  cancelVaultNameEdits(){
    this.isUserEditingVName = false;
    this.newVaultName = null;
  }

  cancelDescriptionEdits(){
    this.isUserEditingVDesc = false;
    this.newDescription = null;
  }

  onStorageKeyUpdated() {
    this.onVaultUpdated.emit();
  }
}
