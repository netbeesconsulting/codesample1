using Stocky.Common;
using Stocky.Core.Domain.Admin;
using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Admin;
using Stocky.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static Stocky.Common.Enums;

namespace Stocky.Domain.DomainModels.Admin
{
    [Table("Product", Schema = "Application")]
    public class Product : BaseAuditable
    {
        [Required]
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        [Required]
        public long CategoryId { get; protected set; }
        public SeperationFactor SeperationFactor { get; protected set; }
        public List<ProductList> ProductList { get; protected set; }
        public List<ProductImage> ProductImages { get; protected set; }


        #region FK
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        #endregion

        public Product()
        {

        }

        public Product(UserModel user)
        {
            _user = user;
        }


        public Product Create(string name, string desc, long categoryId, SeperationFactor seperationFactor)
        {
            AuditableCreate();
            Validate(name);
            Name = name;
            Description = desc;
            CategoryId = categoryId;
            SeperationFactor = seperationFactor;
            return this;
        }
        public Product SetProductImages(List<string> images)
        {
            if (!images.IsNullOrZero())
            {
                if (ProductImages.IsNullOrZero())
                {
                    ProductImages = new List<ProductImage>();
                }
                images.ForEach(item =>
                {
                    ProductImages.Add(new ProductImage().Create(Id, item));
                });
            }
            return this;
        }

        public Product SetProductList(List<ProductList> productList)
        {
            if (!productList.IsNullOrZero())
            {
                if (ProductList.IsNullOrZero())
                {
                    ProductList = new List<ProductList>();
                }
                productList.ForEach(item =>
                {
                    ProductList.Add(new ProductList(_user).Create(Id, item.AvailableQuantity, item.InvoicedPrice,
                                    item.PurchasedPrice, item.SeperationFactorValue, item.ReorderMargin));
                });
            }
            return this;
        }

        public Product AddProductListItem(UserModel user, ProductList item)
        {
            ProductList.Add(new ProductList(user).Create(Id, item.AvailableQuantity, item.InvoicedPrice,
                            item.PurchasedPrice, item.SeperationFactorValue, item.ReorderMargin));
            return this;
        }

        public Product Update(UserModel user, string name, string desc, long categoryId, SeperationFactor seperationFactor)
        {
            _user = user;
            AuditableUpdate();
            Validate(name);
            Name = name;
            Description = desc;
            CategoryId = categoryId;
            SeperationFactor = seperationFactor;
            return this;
        }

        public Product UpdateProductList(UserModel user, List<ProductList> productList, Product product)
        {
            if (!productList.IsNullOrZero())
            {
                if (ProductList.IsNullOrZero())
                {
                    ProductList = new List<ProductList>();
                }

                var savedProductItemList = productList.Select(p => p.SeperationFactorValue).ToList();
                var removedImagesList = ProductList.Where(p => !savedProductItemList.Contains(p.SeperationFactorValue)).ToList();
                var newProductItems = productList.Where(p => p.ProductId == 0).ToList();

                removedImagesList.ForEach(item =>
                {
                    ProductList.Remove(item);
                });

                productList.ForEach(item =>
                {
                    if (!item.Id.IsNullOrZero())
                    {
                        var res = product.ProductList.FirstOrDefault(p => p.Id == item.Id);
                        if (!res.IsNull())
                        {
                            res.Update(user, item.AvailableQuantity, item.InvoicedPrice,
                              item.PurchasedPrice, item.SeperationFactorValue, item.ReorderMargin);
                        }
                    }
                });

                if (!newProductItems.IsNullOrZero())
                {
                    newProductItems.ForEach(obj =>
                    {
                        ProductList.Add(new ProductList(_user).Create(Id, obj.AvailableQuantity, obj.InvoicedPrice,
                                        obj.PurchasedPrice, obj.SeperationFactorValue, obj.ReorderMargin));
                    });
                }
            }
            return this;
        }

        public Product UpdateProductImages(List<ProductImageModel> productImages, List<string> images)
        {
            if (!productImages.IsNullOrZero())
            {
                if (ProductImages.IsNullOrZero())
                {
                    ProductImages = new List<ProductImage>();
                }
                var savedImagesList = productImages.Select(p => p.ImageName).ToList();
                var removedImagesList = ProductImages.Where(p => !savedImagesList.Contains(p.ImageName)).ToList();
                removedImagesList.ForEach(item =>
                {
                        ProductImages.Remove(item);
                });
                if (!images.IsNullOrZero())
                {
                    images.ForEach(item =>
                    {
                        ProductImages.Add(new ProductImage().Create(Id, item));
                    });
                }
            }
            return this;
        }

        public Product Delete(UserModel user)
        {
            _user = user;
            AuditableUpdate();
            IsDeleted = true;
            return this;
        }

        public void Validate(string name)
        {
            if (name.IsNull())
            {
                throw new NullObjectException("name is required");
            }
        }
    }
}
