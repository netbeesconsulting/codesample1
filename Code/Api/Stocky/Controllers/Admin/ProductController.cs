using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Utility;
using System;
using System.Threading.Tasks;

namespace Stocky.Controllers.Admin
{
    [Route(API_VERSION + "/product"), Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost, ModelValidation]
        public async Task<IActionResult> Create()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var productModel = httpRequest.Form["entityData"];
                var request = JsonConvert.DeserializeObject<ProductModel>(productModel);
                var result = await SaveImage("products");
                if (result.FileIsThere)
                {
                    request.Images = result.FileNames;
                }
                await _productService.Add(Request(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpPut, ModelValidation]
        public async Task<IActionResult> Update()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var productModel = httpRequest.Form["entityData"];
                var request = JsonConvert.DeserializeObject<ProductModel>(productModel);
                var result = await SaveImage("products");
                if (result.FileIsThere)
                {
                    request.Images = result.FileNames;
                }
                await _productService.Update(Request(request));
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
                await _productService.Delete(Request(id));
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
                return Ok(await _productService.Get(Request(id)));
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
                return Ok(await _productService.GetAllProducts(Request(searchRequest)));
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
                return Ok(await _productService.KeyValue());
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpPost("addproductlistitem"), ModelValidation]
        public async Task<IActionResult> AddProductListItem([FromBody] ProductListModel request)
        {
            try
            {
                await _productService.AddProductListItem(Request(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}