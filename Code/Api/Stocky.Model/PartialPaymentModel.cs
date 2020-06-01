using System;
using System.ComponentModel.DataAnnotations;
using static Stocky.Common.Enums;

namespace Stocky.Model
{
    public class PartialPaymentModel
    {
        public long Id { get; set; }
        [Required]
        public long PaymentId { get; set; }
        public DateTime PaidDateTime { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
