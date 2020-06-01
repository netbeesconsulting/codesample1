using Microsoft.EntityFrameworkCore;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Common;
using Stocky.Domain.DomainModels.Admin;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task Add(Request<ProductModel> request)
        {
            var product = new Product(request.User).Create(request.Item.Name, request.Item.Description, request.Item.CategoryId, request.Item.SeperationFactor);
            var productList = _injector._mapper.Map<List<ProductList>>(request.Item.ProductList);
            if (productList.IsNull()) throw new Exception("Product items not found");
            product.SetProductImages(request.Item.Images);
            product.SetProductList(productList);
            await _injector._context.Product.AddAsync(product);
            await _injector._context.SaveChangesAsync();
        }

        public async Task Delete(Request<long> request)
        {
            var product = _injector._context.Product.FirstOrDefault(p => p.Id == request.Item);
            product.Delete(request.User);
            await _injector._context.SaveChangesAsync();
        }

        public async Task<ProductModel> Get(Request<long> request)
        {
            var product = await _injector._context.Product
                            .Include(p => p.ProductList)
                            .Include(p => p.ProductImages)
                            .FirstOrDefaultAsync(p => p.Id == request.Item && !p.IsDeleted);
            return _injector._mapper.Map<ProductModel>(product);
        }

        public async Task<PageList<ProductViewResult>> GetAllProducts(Request<SearchRequest> searchRequest)
        {
            var productListQuery = _injector._context.Product.Include(p => p.Category)
                                        .Where(p => !p.IsDeleted).AsQueryable();
            if (!searchRequest.Item.SearchTerm.IsNullOrEmpty())
            {
                searchRequest.Item.SearchTerm = searchRequest.Item.SearchTerm.Trim();
                productListQuery = productListQuery.Where(p => EF.Functions.Like(p.Name, "%" + searchRequest.Item.SearchTerm + "%"));
            }
            var totalRecordCount = await productListQuery.CountAsync();
            var productList = await productListQuery.OrderBy(p => p.Id).Skip(searchRequest.Item.Skip).Take(searchRequest.Item.Take)
                                    .Select(p => new ProductViewResult()
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Category = p.Category.Name,
                                        SeperationFactor = p.SeperationFactor.GetDescription()
                                    }).ToListAsync();

            return new PageList<ProductViewResult>(productList, searchRequest.Item.Skip, searchRequest.Item.Take, totalRecordCount);
        }

        public async Task<List<KeyValuePair<long, string>>> KeyValue()
        {
            return await _injector._context.Product.Where(p => !p.IsDeleted)
                        .Select(p => new KeyValuePair<long, string>(p.Id, p.Name))
                        .ToListAsync();
        }

        public async Task Update(Request<ProductModel> request)
        {
            try
            {
                var product = _injector._context.Product
                          .Include(p => p.ProductList)
                          .Include(p => p.ProductImages)
                          .FirstOrDefault(p => p.Id == request.Item.Id);

                if (product.IsNull()) throw new Exception("Product is not found");

                product.Update(request.User, request.Item.Name, request.Item.Description, request.Item.CategoryId, request.Item.SeperationFactor);
                var productList = _injector._mapper.Map<List<ProductList>>(request.Item.ProductList);
                if (productList.IsNull()) throw new Exception("Product items not found");
                product.UpdateProductList(request.User, productList, product);
                product.UpdateProductImages(request.Item.ProductImages, request.Item.Images);
                await _injector._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task AddProductListItem(Request<ProductListModel> request)
        {
            var product = _injector._context.Product
                          .Include(p => p.ProductList)
                          .FirstOrDefault(p => p.Id == request.Item.ProductId);
            product.AddProductListItem(request.User, _injector._mapper.Map<ProductList>(request.Item));
            await _injector._context.SaveChangesAsync();
        }

        public Task<PageList<ProductModel>> GetAll(Request<SearchRequest> searchRequest)
        {
            throw new NotImplementedException();
        }
    }
}
