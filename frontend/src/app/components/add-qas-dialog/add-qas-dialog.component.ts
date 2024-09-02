import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-qas-dialog',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './add-qas-dialog.component.html',
  styleUrl: './add-qas-dialog.component.scss'
})
export class AddQAsDialogComponent {
  @Input({required: true}) display = false;
  @Input({required:true}) keyGroup!:FormGroup;
  @Output() displayChange = new EventEmitter<boolean>();

  private fb = inject(FormBuilder);

  closeDialog() {
    this.displayChange.emit(this.display=false);
  }

  addNewQuestion(){
    this.QAFormArray.push(this.fb.group({
      question: [null, [Validators.required]],
      answer: [null, [Validators.required]],
    }));
  }

  get QAFormArray(){
    return this.keyGroup.get('securityQAs') as FormArray
  }
}
