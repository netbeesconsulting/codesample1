import { OrderStatus } from '../../core/enums/orderStatus';
import { OrderItemModel } from './orderItemModel';

export class OrderModel {
    id: number;
    orderDate: Date;
    totalAmount: number;
    orderStatus: OrderStatus;
    orderItems = new Array<OrderItemModel>();
    customerId: number;

    constructor() {
        this.orderItems = new Array<OrderItemModel>();
    }
}



