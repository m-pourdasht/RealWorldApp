using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RealWorldApp.Shared.Models;
using System.Collections.Generic;

namespace RealWorldApp.Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product");
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/product/{id}");
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/product", product);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/product/{product.Id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/product/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
