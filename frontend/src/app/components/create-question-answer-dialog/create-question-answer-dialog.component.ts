import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VaultKeyQAService } from '../../shared/services/vault-key-qas/vault-key-qa.service';
import { NotificationService } from '../../shared/services/notification/notification.service';
import { SecurityQuestion } from '../../shared/models/vault.model';

@Component({
  selector: 'app-create-question-answer-dialog',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './create-question-answer-dialog.component.html',
  styleUrl: './create-question-answer-dialog.component.scss'
})
export class CreateQuestionAnswerDialogComponent implements OnInit {

  @Input({required:true}) display = false;
  @Input({required:true}) keyId = '';

  @Output() displayChange = new EventEmitter<boolean>();
  @Output() onQuestionAdded = new EventEmitter<SecurityQuestion>();

  
  private fb = inject(FormBuilder);
  private qaService = inject(VaultKeyQAService);
  private notificationService = inject(NotificationService);
  
  createQuestionForm!:FormGroup;


  ngOnInit(): void {
    this.buildForm()
  }

  private buildForm() {
    this.createQuestionForm = this.fb.group({
      userKey:[null],
      question:[null, [Validators.required]],
      answer:[null, [Validators.required]],
      keyId:[this.keyId, [Validators.required]]
    });
  }

  createQuestion(){
    if(this.createQuestionForm.invalid)
      return;

    var formValue = this.createQuestionForm.value;
    this.qaService.addNewQuestion(formValue).subscribe(({id})=>{
      this.notificationService.showMessage({
        severity: 'success',
        summary: 'Question Added.'
      });
      this.onQuestionAdded.emit({
        vaultStorageKeySecurityQAId: id,
        question: formValue.question,
        answer: formValue.answer,
        vaultStorageKeyId: formValue.keyId,
      });
      this.closeDialog();
    });
  }

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }
} 
