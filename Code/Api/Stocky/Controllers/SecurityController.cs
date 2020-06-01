using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocky.Application.Service.Security.Interface;
using Stocky.Common;
using Stocky.Model.Admin;
using Stocky.Model.Security;
using System;
using System.Threading.Tasks;

namespace Stocky.Controllers
{
    [AllowAnonymous]
    [Route(API_VERSION + "/account")]
    public class SecurityController : BaseController
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            try
            {
                var result = await _securityService.Authenticate(request.Email, request.Password);
                if (result.IsNull()) throw new Exception("Invalid Username Or password");
                var token = await GenerateJwtToken(result);
                return Ok(new { Token = token.ToString() });
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}