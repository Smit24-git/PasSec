import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { VaultService } from '../../shared/services/vaults/vault.service';
import { NotificationService } from '../../shared/services/notification/notification.service';
import { AddQAsDialogComponent } from '../add-qas-dialog/add-qas-dialog.component';

@Component({
  selector: 'app-create-vault-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SharedModule,
    AddQAsDialogComponent],
  templateUrl: './create-vault-dialog.component.html',
  styleUrl: './create-vault-dialog.component.scss'
})
export class CreateVaultDialogComponent implements OnInit {
  @Input({required: true}) display = false;
  @Output() displayChange = new EventEmitter<boolean>();
  
  createVaultForm!: FormGroup;

  private fb = inject(FormBuilder);
  private vaultService = inject(VaultService);
  private notificationService = inject(NotificationService);

  editingKey = '';

  securityKeyFormGroup?:FormGroup;
  displayAddQADialog:boolean = false;
  
  ngOnInit(): void {
    this.buildForm();
  }

  closeDialog() {
    this.displayChange.emit(this.display = false);
  }

  private buildForm(){
    this.createVaultForm = this.fb.group({
      vaultName: [,[Validators.required]],
      description: [],
      useUserKey:[false, [Validators.required]],
      userKey: [{value:null,disabled:true}],
      keys: this.fb.array([]),
    });

    this.createVaultForm.get('useUserKey')?.valueChanges.subscribe(value=>{
      const userKeyValidators = [Validators.required];
      const userKeyControl = this.createVaultForm.controls['userKey'];
      if(value==true) {
        userKeyControl.enable();
        userKeyControl.setValue(null);
        userKeyControl.setValidators(userKeyValidators);
        userKeyControl.updateValueAndValidity();
      } else {
        userKeyControl.setValue(null)
        userKeyControl.disable();
        userKeyControl.removeValidators(userKeyValidators);
      }
    });

    // sample first key
    this.addNewKey();
  }

  addNewKey(){
    let keysFormArray = this.createVaultForm.controls['keys'] as FormArray;
    let randomValue = Math.random();
    keysFormArray.push(this.fb.group({
      _key:[randomValue],
      keyName:[null, [Validators.required]],
      username: [''],
      password: ['', [Validators.required]],
      email: [''],
      accessLocation: [''],
      securityQAs: this.fb.array([]),
    }));
  }

  get keysFormArray(): FormArray{
    return this.createVaultForm.get('keys') as FormArray
  }

  editingRow(row:FormGroup){
    //this.editingKey = row.controls['_key'].value;
  }

  editedRow() {
    this.editingKey = ''; 
  }

  cancelledRowEdit() {
    this.editingKey = '';

  }

  createVault(){
    if(this.createVaultForm.invalid)
      return;
    
    const vaultFormValue = this.createVaultForm.value;

    this.vaultService.createVault(vaultFormValue).subscribe({
      next: (_)=>{
        this.notificationService.showMessage({severity: 'success', summary: 'New vault added securely!'});
        this.closeDialog();
      },
      error: ()=>{
        // handled by global error!
      }
    });
  }

  removeKey(rowIndex:number){
    (this.createVaultForm.get("keys") as FormArray)
    .removeAt(rowIndex);
  }

  addQAs(control:FormGroup){
    this.securityKeyFormGroup = control;
    this.displayAddQADialog = true;
    console.log(control)
  }
}
