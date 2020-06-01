using System;
using System.Collections.Generic;

namespace Stocky.Model.ViewResult
{
    public class PaymentViewResult
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal RemainingAmount { get; set; }
        public string CustomerName { get; set; }
        public List<PartialPaymentViewResult> PartialPayments { get; set; }

        public PaymentViewResult SetPayment(long id, long orderId, decimal totalAmount, string paymentStatus, decimal discount, 
            decimal totalPyable, string customerName,DateTime orderDate)
        {
            Id = id;
            OrderId = orderId;
            TotalAmount = totalAmount;
            PaymentStatus = paymentStatus;
            Discount = discount;
            TotalPayable = totalPyable;
            CustomerName = customerName;
            OrderDate = orderDate;
            return this;
        }
    }
}
