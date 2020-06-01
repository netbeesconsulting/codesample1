import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryViewComponent } from './category-view/category-view.component';
import { CategoryUpdateComponent } from './category-update/category-update.component';
import { CategoryRoutingModule } from './category-routing.module';
import { CategoryService } from './service/category.service';


@NgModule({
  declarations: [CategoryViewComponent, CategoryUpdateComponent],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule
  ],
  providers: [CategoryService]
})
export class CategoryModule { }
