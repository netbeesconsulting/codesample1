import { Component, OnInit, ViewChild, Output, Input, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'app-update-modal',
  templateUrl: './update-modal.component.html',
  styleUrls: ['./update-modal.component.scss']
})
export class UpdateModalComponent implements OnInit {

  @ViewChild('updateConfirmationModal', { static: true }) updateConfirmationModal: ModalDirective;
  @Output() update: EventEmitter<any> = new EventEmitter();
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
    this.updateConfirmationModal.show();
  }

  public hideModal(): void {
    this.updateConfirmationModal.hide();
  }

  confirm(): void {
    this.update.emit(this.modalItem);
    this.updateConfirmationModal.hide();
  }

  reject() {
    this.cancel.emit(this.modalItem);
    this.updateConfirmationModal.hide();
  }

}
