import { PaymentService } from './../service/payment.service';
import { Component, OnInit } from '@angular/core';
import { ExtensionService } from '../../core/services/extension.service';
import { Subject } from 'rxjs';
import { SearchRequestModel } from '../../core/model/searchRequestModel';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-payment-view',
  templateUrl: './payment-view.component.html',
  styleUrls: ['./payment-view.component.scss']
})
export class PaymentViewComponent extends ExtensionService implements OnInit {

  payments = [];
  searchTerm$ = new Subject<string>();
  searchRequestModel = new SearchRequestModel(8);

  constructor(private fb: FormBuilder, private router: Router,
    private paymentService: PaymentService, protected toasterService: ToastrService) {
    super(toasterService);

    this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()).subscribe(data => {
      if (data != "" && data)
        this.searchRequestModel.searchTerm = data;
      else {
        this.searchRequestModel.searchTerm = '';
      }
      this.getPayments();
    });
  }

  ngOnInit() {
    this.getPayments();
  }

  async getPayments() {
    try {
      (await this.paymentService.generateRequest(this.setupReadAll("payment", this.searchRequestModel), HttpOperation.Get))
        .subscribe((res: any) => {
          this.payments = res.items;
          this.searchRequestModel.totalRecords = res.totalRecordCount;
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  async editPayment(id: number) {
    try {
      this.router.navigate(['/payment/update', id]);
    } catch (error) {
      this.warning(error);
    }
  }

  public pageChanged(event: any): void {
    this.searchRequestModel.skip = (event.page - 1) * this.searchRequestModel.take;
    this.searchRequestModel.currentPage = event.page;
    this.getPayments();
  }
}