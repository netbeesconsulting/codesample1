using System;
using System.ComponentModel;

namespace Stocky.Common
{
    public static class Enums
    {
        public enum SeperationFactor
        {
            [Description("None")]
            None = 0,
            [Description("Color")]
            Color = 1,
            [Description("Set")]
            Sets = 2,
            [Description("Weight")]
            Weight = 3
        }
        public enum UserType : byte
        {
            None = 0,
            Admin = 1,
            Customer = 2,
            AdminUser = 3,
        }
        public enum RecordState : byte
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }
        public enum OrderStatus : byte
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Confirmed")]
            Confirmed = 2,
            [Description("Cancelled")]
            Cancelled = 3
        }

        public enum PaymentStatus : byte
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Paid")]
            Paid = 2,
            [Description("PartiallyPaid")]
            PartiallyPaid = 3
        }

        public enum PaymentMethod : byte
        {
            [Description("Cash")]
            Cash = 1,
            [Description("Cheque")]
            Cheque = 2
        }

        public enum ChequeStatus : byte
        {
            Unrealized = 1,
            Realized = 2,
            Bounced = 3,
            UnPresented = 4
        }

        public enum DiscountType : byte
        {
            [Description("None")]
            None = 1,
            [Description("Percentage")]
            Percentage = 2,
            [Description("Value")]
            Value = 3
        }
    }
}
