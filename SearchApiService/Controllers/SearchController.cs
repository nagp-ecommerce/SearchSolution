using Microsoft.AspNetCore.Mvc;
using SearchApiService.DTOs;
using SearchApiService.Interfaces;
using SearchApiService.Models;

namespace SearchApiService.Controllers
{
    [Route("api/search")]
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
            catch (Exception ex)
            {
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
                    new ProductSearchRequest { query = categoryName }
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create-index")]
        public async Task<IActionResult> CreateIndex(ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Request);
            }
            await _searchService.AddOrUpdate(
                    new Product
                    {
                        Brand = product.Brand,
                        ProductName = product.ProductName,
                        Price = product.Price
                    }
                );
            // http://localhost:9200/products/_search will get all search indexes

            return Ok();
        }
    }
}
