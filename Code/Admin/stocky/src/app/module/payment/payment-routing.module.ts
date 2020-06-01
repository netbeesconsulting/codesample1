import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PaymentViewComponent } from './payment-view/payment-view.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'view',
        pathMatch: 'full'
    },
    {
        path: 'view',
        component: PaymentViewComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PaymentRoutingModule { }