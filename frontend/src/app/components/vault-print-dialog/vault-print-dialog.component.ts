import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, input, Input, Output, ViewChild, viewChild } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Vault } from '../../shared/models/vault.model';
import { jsPDF } from "jspdf";

@Component({
  selector: 'app-vault-print-dialog',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './vault-print-dialog.component.html',
  styleUrl: './vault-print-dialog.component.scss'
})
export class VaultPrintDialogComponent {
  @Input({required: true}) display = false;
  @Input({required: true}) vault!:Vault;
  @Output() displayChange = new EventEmitter<boolean>();

  @ViewChild("print") printDiv!:ElementRef;
  date = new Date();

  closeDialog(){
    this.displayChange.emit(this.display = false);
  }

  printVault() {
    const doc = new jsPDF({orientation: 'l',unit: 'px', format: 'letter', userUnit: 1200});
    
    doc.html(this.printDiv.nativeElement,{
      callback: ()=>{
        doc.save("sample.pdf");
      },
      html2canvas: {
        scale:0.45
      },
      margin: [30,30,30,30]
    })
  }
}
