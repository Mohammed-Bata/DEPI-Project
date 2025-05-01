using System.Configuration;
using System.Security.Policy;
using System.Text;
using MVC.Models;
using MVC.Models.Dtos;
using Newtonsoft.Json;
using NuGet.Protocol;
using static Utility.SD;


namespace MVC.Services
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
       
    }
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7225/api";
        
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;    
        }

        public async Task<List<Product>> GetProductsAsync<Product>()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/product");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse<List<Product>>>(json);
            return result.Data;

        }

        public async Task<List<Review>> GetProductWithReviewsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/product/{id}/reviews");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<APIResponse<List<Review>>>(json);
            return result.Data;
        }
        //public async Task<bool> CreateProductAsync(ProductDto model)
        //{
        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(model);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/product", content);
        //        return response.IsSuccessStatusCode;
        //    }
        //    catch (Exception ex) { 
        //    return false;
        //    }
        //}
        public async Task<APIResponse<bool>> CreateProductAsync(ProductDto model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/product", content);

                if (response.IsSuccessStatusCode)
                {
                    return new APIResponse<bool>
                    {
                        IsSuccess = true,
                        Data = true
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new APIResponse<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = errorContent,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Data = false
                };
            }
        }
    }
}
