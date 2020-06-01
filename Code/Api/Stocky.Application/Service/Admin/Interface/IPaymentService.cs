using Stocky.Model;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin.Interface
{
    public interface IPaymentService
    {
        Task<PageList<PaymentViewResult>> GetAll(Request<SearchRequest> searchRequest);
        Task<PaymentViewResult> GetPaymentById(Request<long> request);
        Task AddPartialPayment(Request<PartialPaymentModel> request);
    }
}
