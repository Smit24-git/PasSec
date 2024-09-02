import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SecurityQuestion } from '../../shared/models/vault.model';
import { VaultKeyQAService } from '../../shared/services/vault-key-qas/vault-key-qa.service';
import { NotificationService } from '../../shared/services/notification/notification.service';

@Component({
  selector: 'app-update-question-dialog',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './update-question-dialog.component.html',
  styleUrl: './update-question-dialog.component.scss'
})
export class UpdateQuestionDialogComponent implements OnInit{

  @Input({required:true}) display = false;
  @Input({required:true}) question!:SecurityQuestion;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() onUpdated = new EventEmitter<SecurityQuestion>();
  
  updateQuestionForm!:FormGroup;


  private fb = inject(FormBuilder);
  private qaService = inject(VaultKeyQAService);
  private notificationService = inject(NotificationService);

  ngOnInit(): void {
    this.buildForm();
  }

  updateQuestion(){
    if(this.updateQuestionForm.invalid)
      return;
    const formValue = this.updateQuestionForm.value;
    this.qaService.updateQuestion(formValue.id, formValue).subscribe(()=>{
      this.notificationService.showMessage({severity:'success',summary: 'Question Updated.'});
      this.onUpdated.emit({
        vaultStorageKeySecurityQAId: formValue.id,
        question: formValue.question,
        answer: formValue.answer,
      });
      
      this.closeDialog();
    });
  }
  

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  //#region private methods  
  private buildForm(){
    this.updateQuestionForm = this.fb.group({
      id: [this.question.vaultStorageKeySecurityQAId, [Validators.required]],
      question:[this.question.question,[Validators.required]],
      answer:[this.question.answer,[Validators.required]],
    });
  }
  //#endregion
}
