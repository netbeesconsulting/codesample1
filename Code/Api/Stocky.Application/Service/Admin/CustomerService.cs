using Microsoft.EntityFrameworkCore;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Common;
using Stocky.Core.Domain.Admin;
using Stocky.Domain.DomainModels.Shared.Security;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task Add(Request<CustomerModel> request)
        {
            using (var transaction = _injector._context.Database.BeginTransaction())
            {
                try
                {
                    var user = new User(request.User).Create(request.Item.Username, request.Item.Password, Enums.UserType.Customer);
                    var userResponse = await _injector._context.User.AddAsync(user);
                    await _injector._context.SaveChangesAsync();

                    var item = new Customer(request.User).Create(request.Item.Name, request.Item.Company, request.Item.Address, request.Item.Contact, userResponse.Entity.Id);
                    await _injector._context.Customer.AddAsync(item);
                    await _injector._context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task Delete(Request<long> request)
        {
            var customer = _injector._context.Customer.FirstOrDefault(p => p.Id == request.Item);
            if (customer.IsNull()) throw new Exception("Customer not found");
            customer.Delete(request.User);
            await _injector._context.SaveChangesAsync();
        }

        public async Task<CustomerModel> Get(Request<long> request)
        {
            var customer = _injector._context.Customer.FirstOrDefault(p => p.Id == request.Item && !p.IsDeleted);
            return _injector._mapper.Map<CustomerModel>(customer);
        }

        public async Task<PageList<CustomerModel>> GetAll(Request<SearchRequest> searchRequest)
        {
            var customerListQuery = _injector._context.Customer.Where(p => !p.IsDeleted).AsQueryable();
            if (!searchRequest.Item.SearchTerm.IsNullOrEmpty())
            {
                searchRequest.Item.SearchTerm = searchRequest.Item.SearchTerm.Trim();
                customerListQuery = customerListQuery.Where(p => EF.Functions.Like(p.Name, "%" + searchRequest.Item.SearchTerm + "%") ||
                EF.Functions.Like(p.Company, "%" + searchRequest.Item.SearchTerm + "%"));
            }
            var totalRecordCount = await customerListQuery.CountAsync();
            var customerList = await customerListQuery.OrderBy(p => p.Id).Skip(searchRequest.Item.Skip).Take(searchRequest.Item.Take).ToListAsync();
            return new PageList<CustomerModel>(_injector._mapper.Map<List<CustomerModel>>(customerList), searchRequest.Item.Skip, searchRequest.Item.Take, totalRecordCount);
        }

        public async Task<List<KeyValuePair<long, string>>> KeyValue()
        {
            return _injector._context.Customer.Where(p => !p.IsDeleted)
                        .Select(p => new KeyValuePair<long, string>(p.Id, p.Name))
                        .ToList();
        }

        public async Task Update(Request<CustomerModel> request)
        {
            try
            {
                var customer = _injector._context.Customer.FirstOrDefault(p => p.Id == request.Item.Id);
                customer.Update(request.User, request.Item.Name, request.Item.Company, request.Item.Address, request.Item.Contact);
                await _injector._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
