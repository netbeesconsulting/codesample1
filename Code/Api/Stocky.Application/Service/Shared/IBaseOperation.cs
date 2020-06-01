
using Stocky.Model.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Shared
{
    public interface IBaseOperation<T>
    {
        Task Add(Request<T> request);
        Task<T> Get(Request<long> request);
        Task Delete(Request<long> request);
        Task<PageList<T>> GetAll(Request<SearchRequest> searchRequest);
        Task Update(Request<T> request);
        Task<List<KeyValuePair<long,string>>> KeyValue();
    }
}
