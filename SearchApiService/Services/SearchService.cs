﻿using SearchApiService.Models;
using OpenSearch.Client;
using SearchApiService.DTOs;
using SearchApiService.Interfaces;

namespace SearchApiService.Services
{
    public class SearchService : ISearchService
    {
        private IOpenSearchClient _client;

        public SearchService(IOpenSearchClient client)
        {
            _client = client;
        }
        public async Task AddOrUpdate(Product product)
        {
            var res = await _client.IndexAsync<Product>(product, idx => idx.Index("products"));
        }

        public async Task CreateIndexIfNotExistsAsync(string index)
        {
            if(!_client.Indices.Exists(index).Exists)
                await _client.Indices.CreateAsync(index);
        }

        public async Task<bool> Delete(string index)
        {
            var res = await _client.DeleteAsync<Product>(index, idx => idx.Index("products"));
            return res != null;
        }

        public async Task<Product> Get(string index)
        {
            var res = await _client.GetAsync<Product>(index, idx => idx.Index("products"));
            return res.Source;
        }

        public async Task<ProductSearchResponse> SearchByCategory(ProductSearchRequest req)
        {
            var response = await _client.SearchAsync<Product>(s => s
               .Index("products")
                   .Query(q => q
                       .Match(p => p
                            .Field(f => f.Category)
                            .Query(req.query)
                       )
                   )
               );
            if (!response.IsValid)
            {
                throw new Exception($"Search failed: {response.DebugInformation}");
            }
            return new ProductSearchResponse
            {
                Products = response.Documents,
                TotalCount = response.Total
            };
        }

        public async Task<ProductSearchResponse> SearchByProduct(ProductSearchRequest req)
        {
            var response = await _client.SearchAsync<Product>(s => s
                .Index("products")
                    .Query(q => q
                        .MultiMatch(mm => mm
                            .Query(req.query)
                            .Fields(f => f
                                .Field(f => f.ProductName)
                                .Field(f => f.Description)
                            )
                            .Fuzziness(Fuzziness.EditDistance(2))
                        )
                    )
                );
            if (!response.IsValid)
            {
                throw new Exception($"Search failed: {response.DebugInformation}");
            }
            return new ProductSearchResponse{
                Products = response.Documents,
                TotalCount= response.Total
            };
                    
        }
    }
}
