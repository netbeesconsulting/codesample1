import { Component, OnInit } from '@angular/core';
import { ExtensionService } from '../../core/services/extension.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from '../service/category.service';
import { Subject } from 'rxjs';
import { SearchRequestModel } from '../../core/model/searchRequestModel';
import { HttpOperation } from '../../core/enums/httpOperation';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.scss']
})
export class CategoryViewComponent extends ExtensionService implements OnInit {

  categories = [];
  searchTerm$ = new Subject<string>();
  searchRequestModel = new SearchRequestModel(8);

  isBlocked = false;

  constructor(private fb: FormBuilder, private router: Router,
    private categoryService: CategoryService, protected toasterService: ToastrService) {
    super(toasterService);

    this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()).subscribe(data => {
      if (data != "" && data)
        this.searchRequestModel.searchTerm = data;
      else {
        this.searchRequestModel.searchTerm = '';
      }
      this.getCategories();
    });
  }

  ngOnInit() {
    this.getCategories();
  }

  async getCategories() {
    try {
      this.isBlocked = true;
      (await this.categoryService.generateRequest(this.setupReadAll("category", this.searchRequestModel), HttpOperation.Get))
        .subscribe((res: any) => {
          this.categories = res.items;
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

  async editCategory(id: number) {
    try {
      this.router.navigate(['/category/update', id]);
    } catch (error) {
      this.warning(error);
    }
  }

  public pageChanged(event: any): void {
    this.searchRequestModel.skip = (event.page - 1) * this.searchRequestModel.take;
    this.searchRequestModel.currentPage = event.page;
    this.getCategories();
  }
}
