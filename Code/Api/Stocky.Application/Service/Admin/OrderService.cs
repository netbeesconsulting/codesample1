using Microsoft.EntityFrameworkCore;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Common;
using Stocky.Core.Domain.Admin;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task Add(Request<OrderModel> request)
        {
            try
            {
                var productList = await _injector._context.Product
                                                .Include(p => p.ProductList)
                                                .ToListAsync();
                var orderItemList = request.Item.OrderItems;
                orderItemList.ForEach(item =>
                {
                    var product = productList.FirstOrDefault(p => p.Id == item.ProductId);
                    var productItem = product.ProductList.FirstOrDefault(p => p.Id == item.ProductItemId);
                    productItem.Utilize(item.Quantity);
                    item.UnitPrice = productItem.InvoicedPrice;
                });
                var item = new Order(request.User).Create(request.Item.CustomerId).AddOrderItems(orderItemList).AddToPayment();
                await _injector._context.Order.AddAsync(item);
                await _injector._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task Delete(Request<long> request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SubOrderViewResult> GetSubOrderById(Request<long> request)
        {
            var productList = await _injector._context.Product.Include(p => p.ProductList).ToListAsync();
            var order = await _injector._context.Order
                            .Include(p => p.OrderItems)
                            .Include(p => p.Customer)
                            .FirstOrDefaultAsync(p => p.Id == request.Item && !p.IsDeleted);

            var subOrderViewResult = new SubOrderViewResult().SetOrder(order.Id, order.OrderDate, order.TotalAmount, order.OrderStatus.GetDescription(),
                                                                       order.Customer.Name, order.Customer.Id);
            order.OrderItems.ForEach(item =>
            {
                var product = productList.FirstOrDefault(p => p.Id == item.ProductId);
                var productItem = product.ProductList.FirstOrDefault(p => p.Id == item.ProductItemId);
                subOrderViewResult.OrderItems.Add(new SubOrderItemViewResult()
                {
                    Id = item.Id,
                    ProductName = product.Name + " - " + productItem.SeperationFactorValue,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalAmount,
                    DiscountType = item.DiscountType,
                    DiscountTypeValue = item.DiscountTypeValue,
                    Discount = item.Discount,
                    TotalPayable = item.TotalPayable
                });
            });
            return subOrderViewResult;
        }

        public Task<PageList<OrderModel>> GetAll(Request<SearchRequest> searchRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<PageList<OrderViewResult>> GetAllOrders(Request<SearchRequest> searchRequest)
        {
            var orderListQuery = _injector._context.Order.Include(p => p.Customer)
                                         .Where(p => !p.IsDeleted).AsQueryable();
            if (!searchRequest.Item.SearchTerm.IsNullOrEmpty())
            {
                searchRequest.Item.SearchTerm = searchRequest.Item.SearchTerm.Trim();
                orderListQuery = orderListQuery.Where(p => EF.Functions.Like(p.Customer.Name, "%" + searchRequest.Item.SearchTerm + "%"));
            }
            var totalRecordCount = await orderListQuery.CountAsync();
            var orderList = await orderListQuery.OrderByDescending(p => p.Id).Skip(searchRequest.Item.Skip).Take(searchRequest.Item.Take)
                                    .Select(p => new OrderViewResult()
                                    {
                                        OrderId = p.Id,
                                        OrderDate = p.OrderDate,
                                        CustomerName = p.Customer.Name,
                                        TotalAmount = p.TotalAmount,
                                        OrderStatus = p.OrderStatus.GetDescription()
                                    }).ToListAsync();
            return new PageList<OrderViewResult>(orderList, searchRequest.Item.Skip, searchRequest.Item.Take, totalRecordCount);
        }


        public Task<List<KeyValuePair<long, string>>> KeyValue()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Request<OrderModel> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<OrderModel> Get(Request<long> request)
        {
            throw new NotImplementedException();
        }
    }
}
