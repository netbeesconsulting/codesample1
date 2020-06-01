import { async } from '@angular/core/testing';
import { ExtensionService } from './../../core/services/extension.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { CategoryService } from '../service/category.service';
import { CategoryModel } from '../model/categoryModel';
import { HttpOperation } from '../../core/enums/httpOperation';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-update',
  templateUrl: './category-update.component.html',
  styleUrls: ['./category-update.component.scss']
})
export class CategoryUpdateComponent extends ExtensionService implements OnInit {

  categoryForm: FormGroup;
  category = new CategoryModel();

  id: number;
  isFormSubmitted = false;
  isEdit = false;
  isBlocked = false;

  constructor(private fb: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService, protected toasterService: ToastrService) {
    super(toasterService);
  }

  ngOnInit() {
    this.createForm();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      if (this.id != 0 && this.id != undefined) {
        this.isEdit = true;
        this.getCategoryById();
      }
    });
  }

  createForm() {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required]],
      description: [''],
    });
  }

  async getCategoryById() {
    try {
      this.isBlocked = true;
      (await this.categoryService.generateRequest(this.setup(`category/${this.id}`), HttpOperation.Get))
        .subscribe((res: any) => {
          this.category = res;
          this.patchCategory(this.category);
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

  patchCategory(category: CategoryModel) {
    this.categoryForm.patchValue({
      name: category.name,
      description: category.description
    });
  }

  async save() {
    try {
      this.isFormSubmitted = true;
      if (this.categoryForm.invalid) return;
      this.category = Object.assign({}, this.category, this.categoryForm.value);
      this.isBlocked = true;
      (await this.categoryService.generateRequest(this.setup("category"), HttpOperation.Post, this.category))
        .subscribe((res: any) => {
          this.success("Category has been created successfully!");
          this.isBlocked = false;
          this.router.navigate(['category/view']);
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

  async delete() {
    try {
      this.isBlocked = true;
      (await this.categoryService.generateRequest(this.setup(`category/${this.id}`), HttpOperation.Delete))
        .subscribe((res: any) => {
          this.router.navigate(['category/view']);
          this.isBlocked = false;
          this.success("Category has been deleted successfully!");
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

  async update() {
    try {
      this.isFormSubmitted = true;
      if (this.categoryForm.invalid) return;
      this.category = Object.assign({}, this.category, this.categoryForm.value);
      this.isBlocked = true;
      (await this.categoryService.generateRequest(this.setup("category"), HttpOperation.Put, this.category))
        .subscribe((res: any) => {
          this.success("Category has been updated successfully!");
          this.isBlocked = false;
          this.router.navigate(['category/view']);
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
}
