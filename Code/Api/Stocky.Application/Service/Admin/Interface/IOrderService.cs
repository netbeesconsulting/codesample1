using Stocky.Application.Service.Shared;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin.Interface
{
    public interface IOrderService : IBaseOperation<OrderModel>
    {
        Task<PageList<OrderViewResult>> GetAllOrders(Request<SearchRequest> searchRequest);
        Task<SubOrderViewResult> GetSubOrderById(Request<long> request);
    }
}
