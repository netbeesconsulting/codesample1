using System;
using System.Linq;
using System.Threading.Tasks;
using Stocky.Application.Service.Security.Interface;
using Stocky.Common;
using Stocky.Core;
using Stocky.Model.Admin;
using Stocky.Model.Security;
using Stocky.Model.Utility;

namespace Stocky.Application.Service.Security
{

    public class SecurtiyService : BaseService, ISecurityService
    {

        public SecurtiyService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task<AuthResponse> Authenticate(string email, string password)
        {
            try
            {
                var user = _injector._context.User.FirstOrDefault(p => p.Email == email);
                if (user.IsNull()) throw new Exception("Invalid user");
                if (!user.Validate(password)) throw new Exception("Invalid username or password");
                return new AuthResponse().Create(user.Id, user.Email, user.RecordStatus, user.UserType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
