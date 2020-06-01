using System;

namespace Stocky.Model.ViewResult
{
    public class PartialPaymentViewResult
    {
        public long Id { get; set; }
        public long PaymentId { get; set; }
        public DateTime PaidDateTime { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
