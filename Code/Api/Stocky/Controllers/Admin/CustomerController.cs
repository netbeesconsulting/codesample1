using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Utility;
using System;
using System.Threading.Tasks;

namespace Stocky.Controllers.Admin
{
    [Route(API_VERSION + "/customer"), Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost, ModelValidation]
        public async Task<IActionResult> Create([FromBody] CustomerModel request)
        {
            try
            {
                await _customerService.Add(Request(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpPut, ModelValidation]
        public async Task<IActionResult> Update([FromBody] CustomerModel request)
        {
            try
            {
                await _customerService.Update(Request(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpDelete("{id}"), ModelValidation]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _customerService.Delete(Request(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpGet("{id}"), ModelValidation]
        public async Task<IActionResult> Read(long id)
        {
            try
            {
                return Ok(await _customerService.Get(Request(id)));
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpGet, ModelValidation]
        public async Task<IActionResult> ReadAll(SearchRequest searchRequest)
        {
            try
            {
                return Ok(await _customerService.GetAll(Request(searchRequest)));
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpGet("Keyvalue"), ModelValidation]
        public async Task<IActionResult> KeyValue()
        {
            try
            {
                return Ok(await _customerService.KeyValue());
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}