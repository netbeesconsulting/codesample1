using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Model;
using Stocky.Model.Utility;
using Stocky.Utility;
using System;
using System.Threading.Tasks;

namespace Stocky.Controllers.Admin
{
    [Route(API_VERSION + "/payment"), Authorize]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost, ModelValidation]
        public async Task<IActionResult> Create([FromBody] PartialPaymentModel request)
        {
            try
            {
                await _paymentService.AddPartialPayment(Request(request));
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
                return Ok(await _paymentService.GetPaymentById(Request(id)));
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
                return Ok(await _paymentService.GetAll(Request(searchRequest)));
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}
