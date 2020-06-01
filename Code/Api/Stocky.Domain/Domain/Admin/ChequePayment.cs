using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Security;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Stocky.Common.Enums;

namespace Stocky.Core.Domain.Admin
{
    [Table("ChequePayment", Schema = "Application")]
    public class ChequePayment : BaseAuditable
    {
        public long PartialPaymentId { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public ChequeStatus ChequeStatus { get; set; }

        #region FK
        [ForeignKey("PartialPaymentId")]
        public PartialPayment PartialPayment { get; set; }
        #endregion

        public ChequePayment()
        {

        }

        public ChequePayment(UserModel user)
        {
            _user = user;
        }

        public ChequePayment Create(long partialPaymentId, DateTime chequeDate, string bank, string branch, string chequeNumber,
                                decimal amount, ChequeStatus chequeStatus)
        {
            PartialPaymentId = partialPaymentId;
            ChequeDate = chequeDate;
            Bank = bank;
            Branch = branch;
            ChequeNumber = chequeNumber;
            Amount = amount;
            ChequeStatus = chequeStatus;
            return this;
        }
    }
}
