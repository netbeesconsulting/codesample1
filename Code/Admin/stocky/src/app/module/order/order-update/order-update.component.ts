import { OrderService } from './../service/order.service';
import { OrderItemModel } from './../model/orderItemModel';
import { Component, OnInit } from '@angular/core';
import { ExtensionService } from '../../core/services/extension.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { OrderModel } from '../model/orderModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { HttpOperation } from '../../core/enums/httpOperation';
import { ProductModel } from '../../product/model/productModel';
import { DiscountType } from '../../core/enums/discountType';

@Component({
  selector: 'app-order-update',
  templateUrl: './order-update.component.html',
  styleUrls: ['./order-update.component.scss']
})
export class OrderUpdateComponent extends ExtensionService implements OnInit {

  orderForm: FormGroup;
  product = new ProductModel();
  order = new OrderModel();
  orderItem = new OrderItemModel();
  orderItems = new Array<OrderItemModel>();

  lineItemIndex: number = 0;
  id: number;
  availableQuantity: number = 0;

  customers = [];
  products = [];
  productItems = [];

  isFormSubmitted = false;
  isOrderItemSubmitted = false;
  isEdit = false;
  isDiscountSelected: boolean = false;

  constructor(private fb: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute,
    private orderService: OrderService, protected toasterService: ToastrService) {
    super(toasterService);
  }

  ngOnInit() {
    this.createForm();
    this.initalize();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      if (this.id != 0 && this.id != undefined) {
        this.isEdit = true;
        this.getOrderById();
      }
    });
  }
  async initalize() {
    try {
      forkJoin(
        await this.orderService.generateRequest(this.setup("customer/Keyvalue"), HttpOperation.Get),
        await this.orderService.generateRequest(this.setup("product/Keyvalue"), HttpOperation.Get))
        .subscribe((res: any) => {
          this.customers = res[0];
          this.products = res[1];
        }, error => {
          this.warning(error);
        });
    } catch (error) {
      this.warning(error);
    }
  }

  onProductChange(event) {
    this.getProductItems(event.target.value);
  }

  async getProductItems(productId) {
    try {
      (await this.orderService.generateRequest(this.setup(`product/${productId}`), HttpOperation.Get))
        .subscribe((res: any) => {
          this.productItems = [];
          this.product = res;
          this.product.productList.forEach(element => {
            this.productItems.push({ key: element.id, value: element.seperationFactorValue });
          });
        }, error => {
          this.warning(error);
        });
    } catch (error) {
      this.warning(error);
    }
  }

  onProductItemChange(event) {
    let selectedItemId = event.target.value;
    let selectedItem = this.product.productList.find(p => p.id == selectedItemId);
    this.orderItem.unitPrice = selectedItem.invoicedPrice;
    this.availableQuantity = selectedItem.availableQuantity;
  }

  createForm() {
    this.orderForm = this.fb.group({
      customerId: [0, [Validators.required]],
      orderItems: [[]]
    });
  }

  addOrderItem() {
    this.isOrderItemSubmitted = true;
    if (this.orderItem.quantity > this.availableQuantity || this.orderItem.quantity == undefined) {
      this.warning("Quantity should be less or equal to available quantity");
      return;
    }
    this.orderItem.setProductName(this.product);
    this.orderItem.setTotalAmount();
    this.orderItems.push(this.orderItem);
    this.orderItem = new OrderItemModel();
    this.availableQuantity = 0;
  }

  onDiscountTypeChange(event, item) {
    this.calculateDiscount(item);
    if (event.target.value != DiscountType.None) {
      this.isDiscountSelected = true;
    } else {
      this.isDiscountSelected = false;
    }
  }

  calculateDiscount(item: OrderItemModel) {
    item.calculateDiscount();
  }

  async save() {
    try {
      this.isFormSubmitted = true;
      if (this.orderForm.invalid) return;
      if (this.orderItems.length == 0) return this.warning("Cannot create an order without order items");

      this.order = Object.assign({}, this.order, this.orderForm.value);
      this.order.orderItems = this.orderItems;
      (await this.orderService.generateRequest(this.setup("order"), HttpOperation.Post, this.order))
        .subscribe((res: any) => {
          this.success("Order has been successfully created");
          this.router.navigate(['order/view']);
        }, error => {
          this.warning(error);
        });
    } catch (error) {
      this.warning(error);
    }
  }

  removeLineItem(index: number) {
    this.orderItems.splice(index, 1);
  }

  async getOrderById() {
    try {
      (await this.orderService.generateRequest(this.setup(`order/${this.id}`), HttpOperation.Get))
        .subscribe((res: any) => {
          this.order = res;
          this.patchOrder(this.order);
        }, error => {
          this.warning(error);
        });
    } catch (error) {
      this.warning(error);
    }
  }

  patchOrder(order: OrderModel) {
    this.orderForm.patchValue({
      customerId: order.customerId,
    });
    this.orderItems = order.orderItems;
    var item = this.orderItems.find(p => p.discountType != DiscountType.None);
    if (item != null)
      this.isDiscountSelected = true;
  }



  // async update() {
  //   try {
  //     this.isFormSubmitted = true;
  //     if (this.productForm.invalid) return;

  //     this.product = Object.assign({}, this.product, this.productForm.value);
  //     this.product.productList = this.productItems;

  //     (await this.productService.generateRequest(this.setup("product"), HttpOperation.FilesPut, this.product, this.files))
  //       .subscribe((res: any) => {
  //         this.success("Product has been successfully updated");
  //         //this.router.navigate(['product/view']);
  //       }, error => {
  //         this.warning(error);
  //       });
  //   } catch (error) {
  //     this.warning(error);
  //   }
  // }

}
