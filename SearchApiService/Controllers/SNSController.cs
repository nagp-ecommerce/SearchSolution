using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SearchApiService.Models;
using SearchApiService.Services;
using SharedService.Lib.Interfaces;
using SharedService.Lib.PubSub;

namespace SearchApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNSController : ControllerBase
    {
        private ISearchService _searchService;
        public SNSController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public IActionResult HandleMessage(
            [FromBody] SNSMessage snsMsg
        )
        {
            if (snsMsg.Type == "Notification")
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive=true };
                var eventData = JsonSerializer.Deserialize<Message<Product>>(snsMsg.Message, options);
                if (eventData != null)
                {
                    var (action, product) = (eventData.Action, eventData.Payload);
                    var res = action switch
                    {
                        ProductEvent.CREATED =>  _searchService.AddOrUpdate(product),
                        ProductEvent.UPDATED =>  _searchService.AddOrUpdate(product),
                        ProductEvent.DELETED =>  _searchService.Delete(product?.Id ?? ""),
                        _ => throw new NotImplementedException(),
                    };
                    return Ok();
                }
            }
            return Ok();
        }
    }
}
