using Stocky.Model.Admin;
using Stocky.Model.Security;
using Stocky.Model.Utility;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Security.Interface
{
    public interface ISecurityService
    {       
        Task<AuthResponse> Authenticate(string email, string password);
    }
}
