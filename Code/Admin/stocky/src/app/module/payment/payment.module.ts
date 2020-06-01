import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PaymentRoutingModule } from './payment-routing.module';
import { PaymentViewComponent } from './payment-view/payment-view.component';
import { PaymentUpdateComponent } from './payment-update/payment-update.component';
import { PaymentService } from './service/payment.service';

@NgModule({
    declarations: [PaymentViewComponent, PaymentUpdateComponent],
    imports: [
        CommonModule,
        PaymentRoutingModule,
        SharedModule
    ],
    providers: [PaymentService]
})
export class PaymentModule { }