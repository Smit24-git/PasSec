import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Vault } from '../../shared/models/vault.model';
import { NotificationService } from '../../shared/services/notification/notification.service';
import { VaultStorageService } from '../../shared/services/vault-storage-keys/vault-storage.service';

@Component({
  selector: 'app-add-vault-security-key-dialog',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './add-vault-security-key-dialog.component.html',
  styleUrl: './add-vault-security-key-dialog.component.scss'
})
export class AddVaultSecurityKeyDialogComponent implements OnInit {
  
  @Input({required:true}) display = false;
  @Input({required:true}) vault!:Vault;
  @Output() displayChange = new EventEmitter();
  @Output() onKeyAdded = new EventEmitter();

  addVaultSecurityKeyForm!:FormGroup;

  private fb = inject(FormBuilder);
  private notificationServie = inject(NotificationService);
  private vaultStorageService = inject(VaultStorageService);

  ngOnInit(): void {
    this.buildGroup();
  }

  AddNewKey(){
    if(this.addVaultSecurityKeyForm.invalid)
      return;
    var formValues = this.addVaultSecurityKeyForm.getRawValue();
    this.vaultStorageService.AddNewKey(formValues)
    .subscribe(()=>{
      this.notificationServie.showMessage({severity:'success',summary: 'New Key Added successfully!'});
      this.onKeyAdded.emit();
      this.resetForm();
    });
  }

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  private buildGroup() {
    this.addVaultSecurityKeyForm = this.fb.group({
      vaultId:[this.vault.vaultId, [Validators.required]],
      userKey:[null, ],
      keyName:[null, [Validators.required]],
      username: [null, ],
      password: [null, [Validators.required]],
      email: [null, ],
      accessLocation: [null, ],
      securityQAs: this.fb.array([]),
    });
  }

  addQAForm() {
    this.securityQAs.push(this.fb.group({
      _key: [Math.random()],
      question: [null, [Validators.required]],
      answer: [null, [Validators.required]],
    }));
  }

  private resetForm() {
    this.addVaultSecurityKeyForm.reset()
    this.addVaultSecurityKeyForm.patchValue({      
      vaultId: this.vault.vaultId,
      securityQAs: [],
    });
    this.addVaultSecurityKeyForm.updateValueAndValidity();
  }

  get securityQAs(): FormArray {
    return this.addVaultSecurityKeyForm.get('securityQAs') as FormArray;
  }

  removeQAForm(index:number){
    this.securityQAs.removeAt(index);
    this.addVaultSecurityKeyForm.updateValueAndValidity();
  }
}
