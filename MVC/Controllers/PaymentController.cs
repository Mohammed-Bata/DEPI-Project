using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Services;

namespace MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService = new PaymentService();

        [HttpPost]
        [Authorize(Roles = "")]
        public async Task<IActionResult> CreatePaymentIntent(decimal amount)
        {
            var clientSecret = await _paymentService.CreatePaymentIntent(amount);
            return Json(new { clientSecret = clientSecret });
        }
    }
}
