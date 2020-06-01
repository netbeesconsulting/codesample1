import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { OrderRoutingModule } from './order-routing.module';
import { OrderService } from './service/order.service';
import { OrderViewComponent } from './order-view/order-view.component';
import { OrderUpdateComponent } from './order-update/order-update.component';
import { ViewOrderReceiptComponent } from './view-order-receipt/view-order-receipt.component';

@NgModule({
  declarations: [OrderViewComponent, OrderUpdateComponent, ViewOrderReceiptComponent],
  imports: [
    CommonModule,
    SharedModule,
    OrderRoutingModule
  ],
  providers: [OrderService]
})
export class OrderModule { }
