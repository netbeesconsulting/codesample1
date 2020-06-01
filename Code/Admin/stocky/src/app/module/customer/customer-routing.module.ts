import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CustomerUpdateComponent } from './customer-update/customer-update.component';
import { CustomerViewComponent } from './customer-view/customer-view.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'view',
        pathMatch: 'full'
    },
    {
        path: 'view',
        component: CustomerViewComponent
    },
    {
        path: 'create',
        component: CustomerUpdateComponent
    },
    {
        path: 'update/:id',
        component: CustomerUpdateComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CustomerRoutingModule { }