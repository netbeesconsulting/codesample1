using Microsoft.EntityFrameworkCore;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Common;
using Stocky.Domain.DomainModels.Admin;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task Add(Request<CategoryModel> request)
        {
            try
            {
                var item = new Category(request.User).Create(request.Item.Name, request.Item.Description);
                await _injector._context.Category.AddAsync(item);
                await _injector._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Request<CategoryModel> request)
        {
            var category = _injector._context.Category.FirstOrDefault(p => p.Id == request.Item.Id);
            category.Update(request.User, request.Item.Name, request.Item.Description);
            await _injector._context.SaveChangesAsync();
        }

        public async Task Delete(Request<long> request)
        {
            var category = _injector._context.Category.FirstOrDefault(p => p.Id == request.Item);
            if (category.IsNull()) throw new Exception("Category not found");
            category.Delete(request.User);
            await _injector._context.SaveChangesAsync();
        }

        public async Task<CategoryModel> Get(Request<long> request)
        {
            var category = _injector._context.Category.FirstOrDefault(p => p.Id == request.Item && !p.IsDeleted);
            return _injector._mapper.Map<CategoryModel>(category);
        }

        public async Task<PageList<CategoryModel>> GetAll(Request<SearchRequest> searchRequest)
        {
            var categoryListQuery = _injector._context.Category.Where(p => !p.IsDeleted).AsQueryable();
            if (!searchRequest.Item.SearchTerm.IsNullOrEmpty())
            {
                searchRequest.Item.SearchTerm = searchRequest.Item.SearchTerm.Trim();
                categoryListQuery = categoryListQuery.Where(p => EF.Functions.Like(p.Name, "%" + searchRequest.Item.SearchTerm + "%"));
            }
            var totalRecordCount = await categoryListQuery.CountAsync();
            var categoryList = await categoryListQuery.OrderBy(p => p.Id).Skip(searchRequest.Item.Skip).Take(searchRequest.Item.Take).ToListAsync();
            return new PageList<CategoryModel>(_injector._mapper.Map<List<CategoryModel>>(categoryList), searchRequest.Item.Skip, searchRequest.Item.Take, totalRecordCount);
        }

        public async Task<List<KeyValuePair<long, string>>> KeyValue()
        {
            return _injector._context.Category.Where(p => !p.IsDeleted)
                                        .Select(p => new KeyValuePair<long, string>(p.Id, p.Name))
                                        .ToList();
        }
    }
}
