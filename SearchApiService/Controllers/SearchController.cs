﻿using Microsoft.AspNetCore.Mvc;
using SearchApiService.DTOs;
using SearchApiService.Services;

namespace SearchApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService;
        public SearchController(ISearchService searchService)
        { 
            _searchService = searchService;
        }

        [HttpGet("product")]
        public async Task<IActionResult> SearchByProduct([FromQuery] string productName, [FromQuery] int page, [FromQuery] int pageSize) 
        {
            if (string.IsNullOrEmpty(productName))
            {
                return BadRequest("Product Name cannot be empty");
            }

            try
            {
                var response = await _searchService.SearchByProduct(
                    new ProductSearchRequest
                    {
                        query = productName,
                    }
                );
                return Ok(response);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("category")]
        public async Task<IActionResult> SearchByCategory([FromQuery] string categoryName, [FromQuery] int page, [FromQuery] int pageSize)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Category Name cannot be empty");
            }

            try
            {
                var response = await _searchService.SearchByCategory(
                new ProductSearchRequest
                    {
                        query = categoryName,
                    }
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
