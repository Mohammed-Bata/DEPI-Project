using MVC.Models;
using MVC.Models.Dtos;
using static Utility.SD;

namespace MVC.Services
{
    public class AddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string Url;
        private readonly BaseService _baseService;
        public AddressService(IHttpClientFactory clientFactory, IConfiguration configuration, BaseService baseService)
        {
            _httpClientFactory = clientFactory;
            Url = configuration.GetValue<string>("ServiceUrls:Api");
            _baseService = baseService;
        }
        public async Task<T> GetAllAsync<T>()
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Get,
                Url = Url + "/api/Address",
            });
        }
        public async Task<T> GetAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Get,
                Url = Url + "/api/Address/"+id,
            });
        }
        public async Task<T> CreateAsync<T>(AddressDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = dto,
                Url = Url + "/api/Address",
            });
        }
        public async Task<T> UpdateAsync<T>(int id,AddressDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Put,
                Data = dto,
                Url = Url + "/api/Address/"+id,
            });
        }
        public async Task<T> DeleteAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Delete,
                Url = Url + "/api/Address/"+id,
            });
        }
    }
}
