import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { SearchRequestModel } from '../../core/model/searchRequestModel';
import { ExtensionService } from '../../core/services/extension.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ProductService } from '../service/product.service';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.scss']
})
export class ProductViewComponent extends ExtensionService implements OnInit {

  products = [];
  searchTerm$ = new Subject<string>();
  searchRequestModel = new SearchRequestModel(8);

  isBlocked = false;

  constructor(private fb: FormBuilder, private router: Router,
    private productService: ProductService, protected toasterService: ToastrService) {
    super(toasterService);

    this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()).subscribe(data => {
      if (data != "" && data)
        this.searchRequestModel.searchTerm = data;
      else {
        this.searchRequestModel.searchTerm = '';
      }
      this.getProducts();
    });
  }

  ngOnInit() {
    this.getProducts();
  }

  async getProducts() {
    try {
      this.isBlocked = true;
      (await this.productService.generateRequest(this.setupReadAll("product", this.searchRequestModel), HttpOperation.Get))
        .subscribe((res: any) => {
          this.products = res.items;
          this.searchRequestModel.totalRecords = res.totalRecordCount;
          this.isBlocked = false;
        },
          error => {
            this.isBlocked = false;
            this.warning(error);
          });
    } catch (error) {
      this.isBlocked = false;
      this.warning(error);
    }
  }

  async editProduct(id: number) {
    try {
      this.router.navigate(['/product/update', id]);
    } catch (error) {
      this.warning(error);
    }
  }

  public pageChanged(event: any): void {
    this.searchRequestModel.skip = (event.page - 1) * this.searchRequestModel.take;
    this.searchRequestModel.currentPage = event.page;
    this.getProducts();
  }

}
