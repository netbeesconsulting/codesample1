import { Component, OnInit, ViewChild, Output, Input, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.scss']
})
export class DeleteModalComponent implements OnInit {

  @ViewChild('deleteConfirmationModal', { static: true }) deleteConfirmationModal: ModalDirective;
  @Output() delete: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Input() message: string;
  modalItem: any;

  constructor() { }

  ngOnInit() {
    if (!this.message)
      this.message = 'Are you sure want to delete?';
  }

  public showModal(item): void {
    this.modalItem = item;
    this.deleteConfirmationModal.show();
  }

  public hideModal(): void {
    this.deleteConfirmationModal.hide();
  }

  confirm(): void {
    this.delete.emit(this.modalItem);
    this.deleteConfirmationModal.hide();
  }

  reject() {
    this.cancel.emit(this.modalItem);
    this.deleteConfirmationModal.hide();
  }
}
