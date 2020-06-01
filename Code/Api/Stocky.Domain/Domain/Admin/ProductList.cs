using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Stocky.Domain.DomainModels.Admin
{
    [Table("ProductList", Schema = "Application")]
    public class ProductList : BaseAuditable
    {
        [Required]
        public long ProductId { get; protected set; }
        [Required]
        public int AvailableQuantity { get; protected set; }
        [Required]
        public decimal InvoicedPrice { get; protected set; }
        [Required]
        public decimal PurchasedPrice { get; protected set; }
        public string SeperationFactorValue { get; protected set; }
        public int ReorderMargin { get; protected set; }

        #region FK
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        #endregion

        public ProductList()
        {

        }

        public ProductList(UserModel userModel)
        {
            _user = userModel;
        }
        public ProductList Create(long productId, int availableQty, decimal invoicedPrice, decimal price, string seperationFactorValue,
            int reorderMargin)
        {
            AuditableCreate();
            ProductId = productId;
            AvailableQuantity = availableQty;
            InvoicedPrice = invoicedPrice;
            PurchasedPrice = price;
            SeperationFactorValue = seperationFactorValue;
            ReorderMargin = reorderMargin;
            return this;
        }

        public ProductList Update(UserModel user, int availableQty, decimal invoicedPrice, decimal price, string seperationFactorValue, int reorderMargin)
        {
            _user = user;
            AuditableUpdate();
            AvailableQuantity = availableQty;
            InvoicedPrice = invoicedPrice;
            PurchasedPrice = price;
            SeperationFactorValue = seperationFactorValue;
            ReorderMargin = reorderMargin;
            return this;
        }

        public ProductList Utilize(int requiredQty)
        {
            if (requiredQty > AvailableQuantity)
                throw new InvalidOperationException("Required quantity should be less than the available quantity");

            AvailableQuantity = AvailableQuantity - requiredQty;
            return this;
        }
    }
}
