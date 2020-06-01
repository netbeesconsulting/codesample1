import { ProductViewComponent } from './product-view/product-view.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ProductUpdateComponent } from './product-update/product-update.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'view',
        pathMatch: 'full'
    },
    {
        path: 'view',
        component: ProductViewComponent
    },
    {
        path: 'create',
        component: ProductUpdateComponent
    },
    {
        path: 'update/:id',
        component: ProductUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductRoutingModule { }