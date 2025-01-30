using SearchApiService.DTOs;
using SearchApiService.Models;

namespace SearchApiService.Services
{
    public interface ISearchService
    {
        Task<ProductSearchResponse> SearchByProduct(ProductSearchRequest request);
        Task<ProductSearchResponse> SearchByCategory(ProductSearchRequest request);
        Task CreateIndexIfNotExistsAsync(string index);
        Task<Product> Get(string index);
        Task<bool> Delete(string index);
        Task AddOrUpdate(Product product);
    }
}
