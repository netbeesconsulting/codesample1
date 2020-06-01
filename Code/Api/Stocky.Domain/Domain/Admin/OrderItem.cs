using Stocky.Domain.DomainModels.Admin;
using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Stocky.Common.Enums;

namespace Stocky.Core.Domain.Admin
{
    [Table("OrderItem", Schema = "Application")]
    public class OrderItem : BaseAuditable
    {
        [Required]
        public long ProductId { get; protected set; }
        [Required]
        public long ProductItemId { get; protected set; }
        [Required]
        public decimal UnitPrice { get; protected set; }
        [Required]
        public int Quantity { get; protected set; }
        [Required]
        public decimal TotalAmount { get; protected set; }
        public DiscountType DiscountType { get; protected set; }
        public string DiscountTypeValue { get; protected set; }
        public decimal Discount { get; protected set; }
        public decimal TotalPayable { get; protected set; }
        public long OrderId { get; set; }

        #region FK
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("ProductItemId")]
        public ProductList ProductList { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        #endregion

        public OrderItem()
        {

        }

        public OrderItem(UserModel user)
        {
            _user = user;
        }

        public OrderItem Create(long productId, long productItemId, decimal unitPrice, int qty, DiscountType discountType, string discountTypeValue, decimal discount)
        {
            AuditableCreate();
            ProductId = productId;
            ProductItemId = productItemId;
            UnitPrice = unitPrice;
            Quantity = qty;
            DiscountType = discountType;
            Discount = discount;
            DiscountTypeValue = discountTypeValue;
            TotalAmount = UnitPrice * Quantity;
            TotalPayable = TotalAmount - Discount;
            return this;
        }
    }
}
