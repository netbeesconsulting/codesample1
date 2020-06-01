import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { CustomerService } from './service/customer.service';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerUpdateComponent } from './customer-update/customer-update.component';
import { CustomerViewComponent } from './customer-view/customer-view.component';

@NgModule({
    declarations: [CustomerUpdateComponent, CustomerViewComponent],
    imports: [
        CommonModule,
        CustomerRoutingModule,
        SharedModule
    ],
    providers: [CustomerService]
})
export class CustomerModule { }
