using Microsoft.EntityFrameworkCore;
using Stocky.Core.Domain.Admin;
using Stocky.Domain.DomainModels.Admin;
using Stocky.Domain.DomainModels.Shared.Security;

namespace Stocky.Core
{
    public class TwinlineEntityContext : DbContext
    {
        public TwinlineEntityContext(DbContextOptions<TwinlineEntityContext> options) : base(options)
        {
        }

        #region Security
        public DbSet<User> User { get; set; }
        #endregion

        #region Admin
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Customer> Customer { get; set; }

        #region Order
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PartialPayment> PartialPayment { get; set; }
        public DbSet<ChequePayment> ChequePayment { get; set; }
        #endregion

        #endregion

    }
}
