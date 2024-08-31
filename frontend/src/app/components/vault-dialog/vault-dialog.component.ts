import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Vault, VaultStorageKey } from '../../shared/models/vault.model';
import { PasswordMaskPipe } from '../../shared/pipes/passwordMask/password-mask.pipe';
import {Clipboard, ClipboardModule} from '@angular/cdk/clipboard';
import { AddVaultSecurityKeyDialogComponent } from '../add-vault-security-key-dialog/add-vault-security-key-dialog.component';

@Component({
  selector: 'app-vault-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SharedModule,
    PasswordMaskPipe,
    ClipboardModule,
    AddVaultSecurityKeyDialogComponent,
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

  displayAddVaultStorageKeyDialog = false;


  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  openStorageKeyDialog(storageKey:VaultStorageKey){

  }

  copyToClipboard(value:string){
    // this.clipboard.copy()
  }

  openAddKeyDialog(){
    this.displayAddVaultStorageKeyDialog = true;
  }

  onKeyAdded() {
    this.onVaultUpdated.emit();
  }
}
