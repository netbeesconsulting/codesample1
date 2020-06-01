import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { DeleteModalComponent } from './modals/delete-modal/delete-modal.component';
import { UpdateModalComponent } from './modals/update-modal/update-modal.component';
import { ModalModule } from "ngx-bootstrap";
import { BlockUIComponent } from './modals/block-ui/block-ui.component';

@NgModule({
  declarations: [DeleteModalComponent, UpdateModalComponent, BlockUIComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot()
  ],
  exports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule,
    ModalModule,
    DeleteModalComponent,
    UpdateModalComponent,
    BlockUIComponent
  ],
  providers:[
    DatePipe
  ]
})
export class SharedModule { }
