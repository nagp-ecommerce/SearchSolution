using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SearchApiService.Interfaces;
using SearchApiService.Models;
using SharedService.Lib.Interfaces;
using SharedService.Lib.PubSub;

namespace SearchApiService.Controllers
{
    [Route("api/sns")]
    [ApiController]
    public class SNSController : ControllerBase
    {
        private ISearchService _searchService;
        public SNSController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("handle-message")]
        public async Task<IActionResult> HandleMessageAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var snsMsg = await reader.ReadToEndAsync();

            Console.WriteLine($"Received SNS Message: {snsMsg}");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var eventData = JsonSerializer.Deserialize<Message<Product>>(snsMsg, options);
            if (eventData != null)
            {
                var (action, product) = (eventData.Action, eventData.Payload);
                var res = action switch
                {
                    ProductEvent.CREATED => _searchService.AddOrUpdate(product),
                    ProductEvent.UPDATED => _searchService.AddOrUpdate(product),
                    ProductEvent.DELETED => _searchService.Delete(product?.Id ?? ""),
                    _ => throw new NotImplementedException(),
                };
                return Ok();
            }
            return Ok();
        }
    }
}
