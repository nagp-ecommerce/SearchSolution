using SearchApiService.Models;

namespace SearchApiService.DTOs
{
    public class ProductSearchResponse
    {
        public IEnumerable<Product> Products { get; set; }
        public long TotalCount { get; set; }
    }
}
