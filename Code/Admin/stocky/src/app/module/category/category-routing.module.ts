import { CategoryUpdateComponent } from './category-update/category-update.component';
import { CategoryViewComponent } from './category-view/category-view.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'view',
        pathMatch: 'full'
    },
    {
        path: 'view',
        component: CategoryViewComponent
    },
    {
        path: 'create',
        component: CategoryUpdateComponent
    },
    {
        path: 'update/:id',
        component: CategoryUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryRoutingModule { }