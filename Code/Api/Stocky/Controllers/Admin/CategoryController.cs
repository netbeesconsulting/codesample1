﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Model.Admin;
using Stocky.Model.Utility;
using Stocky.Utility;
using System;
using System.Threading.Tasks;

namespace Stocky.Controllers.Admin
{
    [Route(API_VERSION + "/category"), Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost, ModelValidation]
        public async Task<IActionResult> Create([FromBody] CategoryModel request)
        {
            try
            {
                await _categoryService.Add(Request(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }

        [HttpPut, ModelValidation]
        public async Task<IActionResult> Update([FromBody] CategoryModel request)
        {
            try
            {
                await _categoryService.Update(Request(request));
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
                await _categoryService.Delete(Request(id));
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
                return Ok(await _categoryService.Get(Request(id)));
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
                return Ok(await _categoryService.GetAll(Request(searchRequest)));
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
                return Ok(await _categoryService.KeyValue());
            }
            catch (Exception ex)
            {
                return await HandleException(ex);
            }
        }
    }
}