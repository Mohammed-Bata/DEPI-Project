using API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent(PaymentDto data)
        {
            try
            {
                var clientSecret = await _paymentService.CreatePaymentIntentAsync(data.Amount);
                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = clientSecret
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
