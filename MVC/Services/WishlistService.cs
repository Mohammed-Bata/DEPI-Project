using MVC.Models;
using static Utility.SD;

namespace MVC.Services
{
    public class WishlistService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string Url;
        private readonly BaseService _baseService;
        public WishlistService(IHttpClientFactory clientFactory, IConfiguration configuration, BaseService baseService)
        {
            _httpClientFactory = clientFactory;
            Url = configuration.GetValue<string>("ServiceUrls:Api");
            _baseService = baseService;
        }

        public async Task<T> GetWishlist<T>()
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Get,
                Url = Url + "/api/Wishlist",
            });
        }

        public async Task<T> AddToWishlist<T>(int productId)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Url = Url + "/api/Wishlist/"+productId,
            });
        }

        public async Task<T> RemoveFromWishlist<T>(int productId)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Delete,
                Url = Url + "/api/Wishlist/"+productId,
            });
        }
    }
}
