using System.ComponentModel.DataAnnotations;

namespace Stocky.Model.Admin
{
    public class ProductListModel
    {
        public long Id { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public decimal InvoicedPrice { get; set; }
        [Required]
        public decimal PurchasedPrice { get; set; }
        public string SeperationFactorValue { get; set; }
        public int ReorderMargin { get; set; }
    }
}
