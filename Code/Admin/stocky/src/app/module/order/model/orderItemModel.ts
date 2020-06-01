import { ProductModel } from './../../product/model/productModel';
import { DiscountType } from '../../core/enums/discountType';

export class OrderItemModel {
    id: number;
    productName: string;
    productId: number;
    productItemId: number;
    unitPrice: number;
    quantity: number;
    totalAmount: number;
    discountType: DiscountType;
    discountTypeValue: number;
    discount: number;
    totalPayable: number;
    orderId: number;

    constructor() {
        this.discount = 0;
        this.discountType = DiscountType.None;
        this.totalAmount = 0;
        this.totalPayable = 0;
        this.productId = 0;
        this.quantity = 0;
        this.productItemId = 0;
        this.unitPrice = 0;
    }

    setProductName(product: ProductModel) {
        this.productName = product.name + " - " + product.productList.find(p => p.id == this.productItemId).seperationFactorValue;
    }

    setTotalAmount() {
        this.totalAmount = this.unitPrice * this.quantity;
        this.totalPayable = this.totalAmount;
    }

    calculateDiscount() {
        let type = Number(this.discountType);
        this.setTotalAmount();
        switch (type) {
            case DiscountType.None:
                {
                    this.discountTypeValue = 0;
                    this.discount = 0;
                    break;
                }
            case DiscountType.Percentage:
                {
                    this.discount = (this.totalAmount * this.discountTypeValue) / 100;
                    this.totalPayable = this.totalAmount - this.discount
                    break;
                }
            case DiscountType.Value:
                {
                    this.discount = this.discountTypeValue;
                    this.totalPayable = this.totalAmount - this.discount
                    break;
                }
        }
    }
}