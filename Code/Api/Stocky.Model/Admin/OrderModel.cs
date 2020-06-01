using System;
using System.Collections.Generic;
using static Stocky.Common.Enums;

namespace Stocky.Model.Admin
{
    public class OrderModel
    {
        public long Id { get; set; }
        public DateTime OrderDate { get;  set; }
        public decimal TotalAmount { get;  set; }
        public OrderStatus OrderStatus { get;  set; }
        public List<OrderItemModel> OrderItems { get;  set; }
        public long CustomerId { get; set; }
    }
}
