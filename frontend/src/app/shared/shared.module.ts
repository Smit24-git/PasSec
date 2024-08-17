import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import {InplaceModule} from 'primeng/inplace';
import { CardModule } from 'primeng/card';
import { ChipModule } from 'primeng/chip';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { FileUploadModule } from 'primeng/fileupload';
import { DialogModule } from 'primeng/dialog';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { FieldsetModule } from 'primeng/fieldset';
import { PanelModule } from 'primeng/panel';
import { CalendarModule } from 'primeng/calendar';
import { InputMaskModule } from 'primeng/inputmask';
import { DataViewModule } from 'primeng/dataview';
import {DividerModule} from 'primeng/divider';
import {InputNumberModule} from 'primeng/inputnumber';
import {DropdownFilterOptions, DropdownModule} from 'primeng/dropdown';
import { ContextMenuModule } from 'primeng/contextmenu';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ListboxModule } from 'primeng/listbox';
import { EditorModule } from 'primeng/editor';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { OrderListModule } from 'primeng/orderlist';
import { StepsModule } from 'primeng/steps';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { SelectButtonModule } from 'primeng/selectbutton';
import { TreeModule } from 'primeng/tree';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    PasswordModule,
    InputTextModule,
    ToastModule,
    MenuModule,
    MenubarModule,
    InplaceModule,
    CardModule,
    ChipModule,
    TableModule,
    ToolbarModule,
    FileUploadModule,
    DialogModule,
    CheckboxModule,
    ConfirmDialogModule,
    MultiSelectModule,
    FieldsetModule,
    PanelModule,
    CalendarModule,
    InputMaskModule,
    DataViewModule,
    DividerModule,
    InputNumberModule,
    DropdownModule,
    ContextMenuModule,
    SidebarModule,
    AutoCompleteModule,
    ListboxModule,
    EditorModule,
    InputTextareaModule,
    OrderListModule,
    StepsModule,
    TagModule,
    TooltipModule,
    SelectButtonModule,
    TreeModule,
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    PasswordModule,
    InputTextModule,
    ToastModule,
    MenuModule,
    MenubarModule,
    InplaceModule,
    CardModule,
    ChipModule,
    TableModule,
    ToolbarModule,
    FileUploadModule,
    DialogModule,
    CheckboxModule,
    ConfirmDialogModule,
    MultiSelectModule,
    FieldsetModule,
    PanelModule,
    CalendarModule,
    InputMaskModule,
    DataViewModule,
    DividerModule,
    InputNumberModule,
    DropdownModule,
    ContextMenuModule,
    SidebarModule,
    AutoCompleteModule,
    ListboxModule,
    EditorModule,
    InputTextareaModule,
    OrderListModule,
    StepsModule,
    TagModule,
    TooltipModule,
    SelectButtonModule,
    TreeModule,
  ],
  providers: [
    MessageService,
  ],
})
export class SharedModule { }
