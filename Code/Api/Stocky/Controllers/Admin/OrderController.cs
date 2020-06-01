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
    [Route(API_VERSION + "/order"), Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost, ModelValidation]
        public async Task<IActionResult> Create([FromBody] OrderModel request)
        {
            try
            {
                await _orderService.Add(Request(request));
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
                return Ok(await _orderService.GetSubOrderById(Request(id)));
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
                return Ok(await _orderService.GetAllOrders(Request(searchRequest)));
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}
