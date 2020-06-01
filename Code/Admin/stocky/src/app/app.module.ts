import { CustomerModule } from './module/customer/customer.module';
import { RouterModule } from '@angular/router';
import { LayoutModule } from './module/layout/layout.module';
import { ProductModule } from './module/product/product.module';
import { CategoryModule } from './module/category/category.module';
import { DashboardModule } from './module/dashboard/dashboard.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './module/auth/auth.module';
import { CoreModule } from './module/core/core.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuardService } from './module/auth/auth.guard.service';
import { TokenInterceptor } from './token-interceptor';
import { ToastrModule } from '../../node_modules/ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OrderModule } from './module/order/order.module';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    RouterModule,
    AuthModule,
    CoreModule,
    HttpClientModule,
    DashboardModule,
    CategoryModule,
    ProductModule,
    CustomerModule,
    OrderModule,
    LayoutModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  providers: [
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
