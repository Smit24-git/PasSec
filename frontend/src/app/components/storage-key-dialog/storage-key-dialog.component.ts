import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/services/notification/notification.service';
import { VaultStorageService } from '../../shared/services/vault-storage-keys/vault-storage.service';
import { SecurityQuestion, VaultStorageKey } from '../../shared/models/vault.model';
import { UpdateQuestionDialogComponent } from '../update-question-dialog/update-question-dialog.component';

@Component({
  selector: 'app-storage-key-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SharedModule,
    UpdateQuestionDialogComponent,
  ],
  templateUrl: './storage-key-dialog.component.html',
  styleUrl: './storage-key-dialog.component.scss'
})
export class StorageKeyDialogComponent implements OnInit {
  @Input({required:true}) display = false;
  @Input({required:true}) storageKey!:VaultStorageKey;

  @Output() displayChange = new EventEmitter();
  @Output() onKeyUpdated = new EventEmitter();
  
  updateStorageKeyForm!:FormGroup;

  
  private fb = inject(FormBuilder);
  private notificationServie = inject(NotificationService);
  private vaultStorageService = inject(VaultStorageService);

  isEditModeEnabled = false;
  selectedQuestion?:SecurityQuestion | null;

  displayUpdateQuestionDialog:boolean = false;

  ngOnInit(): void {
    this.buildGroup();
  }

  updateKey(): void {
    if(this.updateStorageKeyForm.invalid)
      return;

    const formValue = this.updateStorageKeyForm.value;
    this.vaultStorageService.updateKey(formValue.vaultStorageKeyId,formValue).subscribe(()=>{
      this.notificationServie.showMessage({severity:'success',summary: 'Key Updated.'});
      this.onKeyUpdated.emit();
      this.storageKey = {
        keyName: formValue.keyName,
        password: formValue.password,
        username: formValue.username,
        emailAddress: formValue.email,
        accessLocation: formValue.accessLocation,
        vaultStorageKeyId: formValue.vaultStorageKeyId,
      }
      this.disableEditMode();
    })
  }

  private buildGroup() {
    this.updateStorageKeyForm = this.fb.group({
      userKey:[null, ],
      vaultStorageKeyId: [this.storageKey.vaultStorageKeyId, [Validators.required]],
      keyName:[this.storageKey.keyName, [Validators.required]],
      username: [this.storageKey.username, ],
      password: [this.storageKey.password, [Validators.required]],
      email: [this.storageKey.emailAddress, ],
      accessLocation: [this.storageKey.accessLocation, ]
    });
    if(this.isEditModeEnabled){
      this.updateStorageKeyForm.enable();
    } else {
      this.updateStorageKeyForm.disable();
    }
  }

  closeDialog() {
    this.displayChange.emit(this.display = false);
  }

  enableEditMode() {
    this.updateStorageKeyForm.enable();
    this.isEditModeEnabled = true;
  }
  disableEditMode() {
    this.updateStorageKeyForm.reset();
    this.updateStorageKeyForm.setValue({
      userKey:null,
      vaultStorageKeyId: this.storageKey.vaultStorageKeyId,
      keyName: this.storageKey.keyName,
      username: this.storageKey.username,
      password: this.storageKey.password,
      email: this.storageKey.emailAddress,
      accessLocation: this.storageKey.accessLocation,
    });
    this.updateStorageKeyForm.disable();
    this.isEditModeEnabled = false;
  }

  openUpdateQuestionDialog(){
    this.displayUpdateQuestionDialog=true;
  }

  onQuestionUpdated(updatedQuestion:SecurityQuestion){
    this.onKeyUpdated.emit();
    var question = this.storageKey.securityQAs?.find(x=>x.vaultStorageKeySecurityQAId == updatedQuestion.vaultStorageKeySecurityQAId);
    if(question){
      question.question = updatedQuestion.question;
      question.answer = updatedQuestion.answer;  
    }
    this.disableEditMode();
  }
}
