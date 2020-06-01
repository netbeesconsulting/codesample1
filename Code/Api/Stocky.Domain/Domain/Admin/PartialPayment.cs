using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Stocky.Common.Enums;

namespace Stocky.Core.Domain.Admin
{
    [Table("PartialPayment", Schema = "Application")]
    public class PartialPayment : BaseAuditable
    {
        [Required]
        public long PaymentId { get; protected set; }
        public DateTime PaidDateTime { get; protected set; }
        [Required]
        public decimal PaidAmount { get; protected set; }
        [Required]
        public PaymentMethod PaymentMethod { get; protected set; }
        public ChequePayment ChequePayment { get; set; } = new ChequePayment();

        #region FK
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        #endregion

        public PartialPayment()
        {

        }

        public PartialPayment(UserModel user)
        {
            _user = user;
        }

        public PartialPayment Create(long paymentId,decimal paidAmount, PaymentMethod paymentMethod)
        {
            AuditableCreate();
            PaidDateTime = DateTime.Now;
            PaymentId = paymentId;
            PaidAmount = paidAmount;
            PaymentMethod = paymentMethod;
            return this;
        }

        public PartialPayment AddCheque(DateTime chequeDate, string bank, string branch, string chequeNumber,
                                decimal amount, ChequeStatus chequeStatus)
        {
            if(PaymentMethod != PaymentMethod.Cheque)
            {
                throw new InvalidOperationException("Payment method should be cheque to add cheques");
            }
            var chequePayment = new ChequePayment(_user).Create(Id, chequeDate, bank, branch, chequeNumber, amount, chequeStatus);
            return this;
        }
    }
}
