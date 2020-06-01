using System.ComponentModel.DataAnnotations;
using static Stocky.Common.Enums;

namespace Stocky.Model.Admin
{
    public class OrderItemModel
    {
        public long Id { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public long ProductItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public string DiscountTypeValue { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public long OrderId { get; set; }
    }
}
