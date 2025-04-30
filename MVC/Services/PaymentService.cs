using System.Text.Json;
using System.Text;
using Models;

namespace MVC.Services
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7225/api/Payment/")
            };
        }
        public async Task<string> CreatePaymentIntent(decimal amount)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { amount = amount }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to create payment intent.");

            var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();
            Console.WriteLine(apiResponse.Data.GetType().Name);
            return apiResponse.Data.ToString();
        }
    }
}
