import { ProductService } from './service/product.service';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductUpdateComponent } from './product-update/product-update.component';
import { ProductRoutingModule } from './product-routing.module';


@NgModule({
  declarations: [ProductViewComponent, ProductUpdateComponent],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule
  ],
  providers: [ProductService]
})
export class ProductModule { }
