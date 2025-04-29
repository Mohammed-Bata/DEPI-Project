using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7225"; // API

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Product");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }

        public async Task<Product> GetProductWithReviewsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Product/{id}/reviews");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(json);
        }
    }
}
