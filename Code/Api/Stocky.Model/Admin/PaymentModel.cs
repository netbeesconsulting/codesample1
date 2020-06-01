using System.ComponentModel.DataAnnotations;
using static Stocky.Common.Enums;

namespace Stocky.Model.Admin
{
    public class PaymentModel
    {
        public long OrderId { get; protected set; }
        [Required]
        public decimal TotalAmount { get; protected set; }
        [Required]
        public PaymentStatus PaymentStatus { get; protected set; }
        public decimal Discount { get; protected set; }
        [Required]
        public decimal TotalPayable { get; protected set; }
        public long CustomerId { get; set; }
    }
}
