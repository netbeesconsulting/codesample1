using Stocky.Domain.DomainModels.Admin;
using Stocky.Domain.DomainModels.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stocky.Core.Domain.Admin
{
    [Table("ProductImage", Schema = "Application")]
    public class ProductImage : BaseEntity
    {
        public string ImageName { get; protected set; }
        public long ProductId { get; protected set; }

        #region FK
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        #endregion

        public ProductImage Create(long productId,string imageName)
        {
            ProductId = productId;
            ImageName = imageName;
            return this;
        }
    }
}
