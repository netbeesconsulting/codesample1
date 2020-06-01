using Stocky.Common;
using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Admin;
using Stocky.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Stocky.Common.Enums;

namespace Stocky.Core.Domain.Admin
{
    [Table("Order", Schema = "Application")]
    public class Order : BaseAuditable
    {
        [Required]
        public DateTime OrderDate { get; protected set; }
        [Required]
        public decimal TotalAmount { get; protected set; }
        [Required]
        public OrderStatus OrderStatus { get; protected set; }
        public virtual List<OrderItem> OrderItems { get; protected set; }
        public Payment Payment { get; set; } = new Payment();
        public long CustomerId { get; set; }

        #region FK
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        #endregion

        public Order()
        {

        }

        public Order(UserModel user)
        {
            _user = user;
        }

        public Order Create(long customerId)
        {
            AuditableCreate();
            OrderDate = DateTime.Now;
            OrderStatus = OrderStatus.Confirmed;
            CustomerId = customerId;
            return this;
        }

        public Order AddOrderItems(List<OrderItemModel> orderItems)
        {
            if (!orderItems.IsNullOrZero())
            {
                if (OrderItems.IsNullOrZero())
                {
                    OrderItems = new List<OrderItem>();
                }
                orderItems.ForEach(item =>
                {
                    OrderItems.Add(new OrderItem(_user).Create(item.ProductId, item.ProductItemId, item.UnitPrice, item.Quantity, item.DiscountType,
                        item.DiscountTypeValue, item.Discount));
                });
            }
            else
            {
                throw new RecordNotFoundException("Order items not found");
            }
            return this;
        }


        public Order AddToPayment()
        {
            var totalAmount = 0.0m;
            var totalDiscount = 0.0m;

            OrderItems.ForEach(item =>
            {
                totalAmount += item.UnitPrice * item.Quantity;
                totalDiscount += item.Discount;
            });
            TotalAmount = totalAmount - totalDiscount;
            Payment = new Payment(_user).Create(Id, totalAmount, totalDiscount, CustomerId);
            return this;
        }
    }
}
