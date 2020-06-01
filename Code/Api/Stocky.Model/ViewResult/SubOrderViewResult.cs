using Stocky.Common;
using Stocky.Model.Admin;
using System;
using System.Collections.Generic;

namespace Stocky.Model.ViewResult
{
    public class SubOrderViewResult
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public virtual List<SubOrderItemViewResult> OrderItems { get; set; } = new List<SubOrderItemViewResult>();
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }

        public SubOrderViewResult SetOrder(long id,DateTime orderDate,decimal totalAmount,string orderStatus,string customerName,long customerId)
        {
            Id = id;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            OrderStatus = orderStatus;
            CustomerName = customerName;
            CustomerId = customerId;
            return this;
        }
    }

    public class SubOrderItemViewResult
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public Enums.DiscountType DiscountType { get; set; }
        public string DiscountTypeValue { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPayable { get; set; }
    }
}