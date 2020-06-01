import { OrderService } from './../service/order.service';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { SearchRequestModel } from '../../core/model/searchRequestModel';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ExtensionService } from '../../core/services/extension.service';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent extends ExtensionService implements OnInit {

  orders = [];
  searchTerm$ = new Subject<string>();
  searchRequestModel = new SearchRequestModel(8);

  constructor(private fb: FormBuilder, private router: Router,
    private orderService: OrderService, protected toasterService: ToastrService) {
    super(toasterService);

    this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()).subscribe(data => {
      if (data != "" && data)
        this.searchRequestModel.searchTerm = data;
      else {
        this.searchRequestModel.searchTerm = '';
      }
      this.getOrders();
    });
  }

  ngOnInit() {
    this.getOrders();
  }

  async getOrders() {
    try {
      (await this.orderService.generateRequest(this.setupReadAll("order", this.searchRequestModel), HttpOperation.Get))
        .subscribe((res: any) => {
          this.orders = res.items;
          this.searchRequestModel.totalRecords = res.totalRecordCount;
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  async editOrder(id: number) {
    try {
      this.router.navigate(['/order/update', id]);
    } catch (error) {
      this.warning(error);
    }
  }

  public pageChanged(event: any): void {
    this.searchRequestModel.skip = (event.page - 1) * this.searchRequestModel.take;
    this.searchRequestModel.currentPage = event.page;
    this.getOrders();
  }
}
