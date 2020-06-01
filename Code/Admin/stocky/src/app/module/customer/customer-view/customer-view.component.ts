import { CustomerService } from './../service/customer.service';
import { Component, OnInit } from '@angular/core';
import { ExtensionService } from '../../core/services/extension.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { SearchRequestModel } from '../../core/model/searchRequestModel';
import { Subject } from 'rxjs';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-customer-view',
  templateUrl: './customer-view.component.html',
  styleUrls: ['./customer-view.component.scss']
})
export class CustomerViewComponent extends ExtensionService implements OnInit {

  customers = [];
  searchTerm$ = new Subject<string>();
  searchRequestModel = new SearchRequestModel(8);

  constructor(private fb: FormBuilder, private router: Router,
    private customerService: CustomerService, protected toasterService: ToastrService) {
    super(toasterService);

    this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()).subscribe(data => {
      if (data != "" && data)
        this.searchRequestModel.searchTerm = data;
      else {
        this.searchRequestModel.searchTerm = '';
      }
      this.getCustomers();
    });
  }

  ngOnInit() {
    this.getCustomers();
  }

  async getCustomers() {
    try {
      (await this.customerService.generateRequest(this.setupReadAll("customer", this.searchRequestModel), HttpOperation.Get))
        .subscribe((res: any) => {
          this.customers = res.items;
          this.searchRequestModel.totalRecords = res.totalRecordCount;
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  async editCustomer(id: number) {
    try {
      this.router.navigate(['/customer/update', id]);
    } catch (error) {
      this.warning(error);
    }
  }

  public pageChanged(event: any): void {
    this.searchRequestModel.skip = (event.page - 1) * this.searchRequestModel.take;
    this.searchRequestModel.currentPage = event.page;
    this.getCustomers();
  }
}
