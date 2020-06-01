using Stocky.Application.Service.Shared;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin.Interface
{
    public interface IProductService : IBaseOperation<ProductModel>
    {
        Task AddProductListItem(Request<ProductListModel> request);
        Task<PageList<ProductViewResult>> GetAllProducts(Request<SearchRequest> searchRequest);
    }
}
