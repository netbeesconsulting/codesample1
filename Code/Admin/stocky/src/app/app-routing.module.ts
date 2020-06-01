import { PaymentModule } from './module/payment/payment.module';
import { OrderModule } from './module/order/order.module';
import { DashboardModule } from './module/dashboard/dashboard.module';
import { ProductModule } from './module/product/product.module';
import { CategoryModule } from './module/category/category.module';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './module/auth/login/login.component';
import { LayoutComponent } from './module/layout/layout/layout.component';
import { AuthGuardService } from './module/auth/auth.guard.service';
import { CustomerModule } from './module/customer/customer.module';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => DashboardModule,
      },
      {
        path: 'category',
        loadChildren: () => CategoryModule,
        canActivate: [AuthGuardService],
      },
      {
        path: 'product',
        loadChildren: () => ProductModule,
        canActivate: [AuthGuardService],
      },
      {
        path: 'customer',
        loadChildren: () => CustomerModule,
        canActivate: [AuthGuardService],
      },
      {
        path: 'order',
        loadChildren: () => OrderModule,
        canActivate: [AuthGuardService],
      },
      {
        path: 'payment',
        loadChildren: () => PaymentModule,
        canActivate: [AuthGuardService],
      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
