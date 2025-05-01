using Newtonsoft.Json;
using Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Dtos;
using MVC.Models;
using static Utility.SD;
namespace MVC.Services
{
    public class ReviewService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string Url;
        private readonly BaseService _baseService;
        public ReviewService(IHttpClientFactory clientFactory, IConfiguration configuration, BaseService baseService)
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
                Url = Url + "/api/Review",
            });
        }
        public async Task<T> GetAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Get,
                Url = Url + "/api/Review/" + id,
            });
        }
        public async Task<T> CreateAsync<T>(ReviewDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = dto,
                Url = Url + "/api/Review",
            });
        }
        public async Task<T> UpdateAsync<T>(int id, ReviewDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Put,
                Data = dto,
                Url = Url + "/api/Review/" + id,
            });
        }
        public async Task<T> DeleteAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Delete,
                Url = Url + "/api/Review/" + id,
            });
        }
    }
}
