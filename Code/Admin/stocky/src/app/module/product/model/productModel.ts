export class ProductModel {
    id: number;
    name: string
    description: string;
    categoryId: number;
    seperationFactor: number;
    productList: Array<ProductItem>;
    productImages = new Array<ProductImage>();

    constructor() {
        this.productList = new Array<ProductItem>();
    }
}

export class ProductItem {
    id: number;
    productId: number;
    availableQuantity: number;
    invoicedPrice: number;
    purchasedPrice: number;
    seperationFactorValue: string;
    reorderMargin: number;
}

export class ProductImage {
    imageName: string = "";
}