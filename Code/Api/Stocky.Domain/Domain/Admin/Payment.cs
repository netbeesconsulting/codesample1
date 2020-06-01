using Stocky.Domain.DomainModels.Shared;
using Stocky.Model;
using Stocky.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Stocky.Common.Enums;

namespace Stocky.Core.Domain.Admin
{
    [Table("Payment", Schema = "Application")]
    public class Payment : BaseAuditable
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
        public List<PartialPayment> PartialPayment { get; set; } = new List<PartialPayment>();

        #region FK
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        #endregion

        public Payment()
        {

        }

        public Payment(UserModel user)
        {
            _user = user;
        }

        public Payment Create(long orderId, decimal totalAmount, decimal discount, long customerId)
        {
            AuditableCreate();
            CustomerId = customerId;
            PaymentStatus = PaymentStatus.Pending;
            OrderId = orderId;
            TotalAmount = totalAmount;
            Discount = discount;
            TotalPayable = TotalAmount - Discount;
            return this;
        }

        public Payment AddPartialPayment(UserModel doingBy,PartialPaymentModel item)
        {
            PartialPayment.Add(new PartialPayment(doingBy).Create(item.PaymentId, item.PaidAmount, item.PaymentMethod));
            return this;
        }
    }
}
