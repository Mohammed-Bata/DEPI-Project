using MVC.Models;
using System.Text.Json;
using System.Text;
using Services.DTOs;
using Humanizer;
using static Utility.SD;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MVC.Services
{
    public class CartService
    {
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private string Url;
        private readonly BaseService _baseService;
        public CartService(IHttpClientFactory clientFactory, IConfiguration configuration, BaseService baseService)
        {
            _httpClientFactory = clientFactory;
            Url = configuration.GetValue<string>("ServiceUrls:Api");
            _baseService = baseService;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var response = await _baseService.SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiType.Get,
                Url = $"{Url}/api/Cart/GetCart?userId={userId}",  // Updated endpoint path
            });

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.Data is JObject jObject)
                {
                    var cartDto = jObject.ToObject<CartDto>();
                    return cartDto ?? new CartDto();
                }
            }
            return new CartDto();
        }

        public async Task<bool> AddItemToCartAsync(string userId, int productId, int quantity)
        {
            var response = await _baseService.SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = new { Id = productId, Quantity = quantity },
                Url = $"{Url}/api/Cart/AddItemToCartAsync?userId={userId}",
            });
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> UpdateQuantityAsync(int cartItemId, int quantity)
        {
            var response = await _baseService.SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiType.Put,
                Data = new { Id = cartItemId, Quantity = quantity },
                Url = $"{Url}/api/Cart/UpdateQuantity",
            });
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            var response = await _baseService.SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiType.Delete,
                Url = $"{Url}/api/Cart/RemoveItemFromCart?cartItemId={cartItemId}",
            });
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var response = await _baseService.SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiType.Delete,
                Url = $"{Url}/api/Cart/ClearCart?userId={userId}",
            });
            return response.StatusCode == HttpStatusCode.OK;
        }

    }
}
