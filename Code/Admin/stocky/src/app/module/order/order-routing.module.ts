import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { OrderViewComponent } from './order-view/order-view.component';
import { OrderUpdateComponent } from './order-update/order-update.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'view',
        pathMatch: 'full'
    },
    {
        path: 'view',
        component: OrderViewComponent
    },
    {
        path: 'create',
        component: OrderUpdateComponent
    },
    {
        path: 'update/:id',
        component: OrderUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrderRoutingModule { }