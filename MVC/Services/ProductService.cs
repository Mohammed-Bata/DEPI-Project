//using Newtonsoft.Json;

//namespace MVC.Services
//{
//    public class ProductService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly string _apiBaseUrl = "http://localhost:5276/API"; // API

//        public ProductService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<List<Product>> GetProductsAsync()
//        {
//            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/products");
//            response.EnsureSuccessStatusCode();
//            var json = await response.Content.ReadAsStringAsync();
//            return JsonConvert.DeserializeObject<List<Product>>(json);
//        }

//        public async Task<Product> GetProductWithReviewsAsync(int id)
//        {
//            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/products/{id}/reviews");
//            response.EnsureSuccessStatusCode();
//            var json = await response.Content.ReadAsStringAsync();
//            return JsonConvert.DeserializeObject<Product>(json);
//        }
//    }
//}
