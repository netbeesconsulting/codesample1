using Stocky.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stocky.Model.Admin
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Required]
        public long CategoryId { get; set; }
        public Enums.SeperationFactor SeperationFactor { get; set; }
        public List<ProductListModel> ProductList { get; set; }
        public List<string> Images { get; set; }
        public List<ProductImageModel> ProductImages { get; set; }
    }
}
