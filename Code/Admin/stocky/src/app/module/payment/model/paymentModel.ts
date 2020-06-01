export class PaymentModel {
    id: number;
    orderId: number;
    totalAmount: number;
    paymentStatus: string;
    discount: number;
    totalPayable: number;
    remainingAmount: number;
    customerName: string;
}