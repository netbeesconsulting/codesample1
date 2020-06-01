using System;

namespace Stocky.Model.ViewResult
{
    public class OrderViewResult
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
