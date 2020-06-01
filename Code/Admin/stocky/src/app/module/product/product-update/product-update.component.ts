import { Component, OnInit } from '@angular/core';
import { ExtensionService } from '../../core/services/extension.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductModel, ProductItem } from '../model/productModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from '../service/product.service';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.scss']
})
export class ProductUpdateComponent extends ExtensionService implements OnInit {

  productForm: FormGroup;
  productItemForm: FormGroup;
  product = new ProductModel();
  productItem = new ProductItem();
  productItems = new Array<ProductItem>();
  urls = new Array<string>();
  urlCount: number = 0;
  lineItemIndex: number = 0;
  categories = [];
  files: FileList;

  id: number;
  isFormSubmitted = false;
  isProductItemFormSubmitted = false;
  isEdit = false;
  isFilesSelected = false;
  isLineItemEdit = false;
  isBlocked = false;

  constructor(private fb: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute,
    private productService: ProductService, protected toasterService: ToastrService) {
    super(toasterService);
  }

  ngOnInit() {
    this.createForm();
    this.initalize();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      if (this.id != 0 && this.id != undefined) {
        this.isEdit = true;
        this.getProductById();
      }
    });
  }
  async initalize() {
    try {
      this.isBlocked = true;
      (await this.productService.generateRequest(this.setup("category/Keyvalue"), HttpOperation.Get))
        .subscribe((res: any) => {
          this.categories = res;
          this.isBlocked = false;
        }, error => {
          this.isBlocked = false;
          this.warning(error);
        });
    } catch (error) {
      this.isBlocked = false;
      this.warning(error);
    }
  }

  detectFiles(event) {
    this.isFilesSelected = false;
    let files = this.files = event.target.files;
    this.urlCount += files.length;
    if (this.urlCount > this.MaximumImagesAllowedCount) {
      this.isFilesSelected = true;
      this.urlCount = this.urls.length;
      return this.warning("Maximum images allowed is 5");
    }
    if (files) {
      this.isFilesSelected = true;
      for (let file of files) {
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        }
        reader.readAsDataURL(file);
      }
    }
  }

  deleteImage(url: any) {
    this.urls = this.urls.filter((a) => a !== url);
    this.product.productImages = this.product.productImages.filter((p) => p.imageName !== url.substr(url.lastIndexOf('/') + 1));
    this.urlCount -= 1;
  }

  createForm() {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      description: [''],
      categoryId: [0, [Validators.required]],
      seperationFactor: [0, [Validators.required]],
      productList: [[]]
    });

    this.productItemForm = this.fb.group({
      id: [0],
      productId: [0],
      seperationFactorValue: [''],
      purchasedPrice: [0, [Validators.required]],
      invoicedPrice: [0, [Validators.required]],
      availableQuantity: [0, [Validators.required]],
      reorderMargin: [0]
    });
  }

  addLineItem() {
    this.isProductItemFormSubmitted = true;
    if (this.productItemForm.invalid) return;
    this.productItem = Object.assign({}, this.productItem, this.productItemForm.value);
    this.productItems.push(this.productItem);
  }

  updateLineItem() {
    this.isProductItemFormSubmitted = true;
    if (this.productItemForm.invalid) return;
    this.productItems[this.lineItemIndex] = Object.assign({}, this.productItem, this.productItemForm.value);
    this.isProductItemFormSubmitted = false;
    this.productItemForm.reset();
    this.isLineItemEdit = false;
  }

  async getProductById() {
    try {
      this.isBlocked = true;
      (await this.productService.generateRequest(this.setup(`product/${this.id}`), HttpOperation.Get))
        .subscribe((res: any) => {
          this.product = res;
          this.patchProduct(this.product)
          this.productItems = this.product.productList;
          this.isBlocked = false;
        }, error => {
          this.isBlocked = false;
          this.warning(error);
        });
    } catch (error) {
      this.isBlocked = false;
      this.warning(error);
    }
  }

  async save() {
    try {
      this.isFormSubmitted = true;
      if (this.productForm.invalid) return;

      this.product = Object.assign({}, this.product, this.productForm.value);
      this.product.productList = this.productItems;
      this.isBlocked = true;
      (await this.productService.generateRequest(this.setup("product"), HttpOperation.FilesPost, this.product, this.files))
        .subscribe((res: any) => {
          this.isBlocked = false;
          this.success("Product has been successfully created");
        }, error => {
          this.isBlocked = false;
          this.warning(error);
        });
    } catch (error) {
      this.isBlocked = false;
      this.warning(error);
    }
  }

  patchProduct(product: ProductModel) {
    this.productForm.patchValue({
      name: product.name,
      description: product.description,
      categoryId: product.categoryId,
      seperationFactor: product.seperationFactor,
    });
    this.urls = this.renderImage("products", product.productImages.map(({ imageName }) => imageName));
    this.isFilesSelected = true;
    this.urlCount = this.urls.length;
  }

  removeLineItem(index: number) {
    this.productItems.splice(index, 1);
  }

  editLineItem(index: number) {
    this.lineItemIndex = index;
    this.isLineItemEdit = true;
    this.patchLineItem(this.productItems[index]);
  }

  patchLineItem(productItem: ProductItem) {
    this.productItemForm.patchValue({
      id: productItem.id,
      productId: productItem.productId,
      seperationFactorValue: productItem.seperationFactorValue,
      purchasedPrice: productItem.purchasedPrice,
      invoicedPrice: productItem.invoicedPrice,
      availableQuantity: productItem.availableQuantity,
      reorderMargin: productItem.reorderMargin
    });
  }

  async delete() {
    try {
      this.isBlocked = true;
      (await this.productService.generateRequest(this.setup(`product/${this.id}`), HttpOperation.Delete))
        .subscribe((res: any) => {
          this.success("Product has been successfully deleted");
          this.isBlocked = false;
          this.router.navigate(['product/view']);
        }, error => {
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
      if (this.productForm.invalid) return;

      this.product = Object.assign({}, this.product, this.productForm.value);
      this.product.productList = this.productItems;

      this.isBlocked = true;
      (await this.productService.generateRequest(this.setup("product"), HttpOperation.FilesPut, this.product, this.files))
        .subscribe((res: any) => {
          this.success("Product has been successfully updated");
          this.isBlocked = false;
          this.router.navigate(['product/view']);
        }, error => {
          this.isBlocked = false;
          this.warning(error);
        });
    } catch (error) {
      this.isBlocked = false;
      this.warning(error);
    }
  }
}
