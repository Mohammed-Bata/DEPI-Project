using Services.Contracts;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    internal class PaymentService : IPaymentService
    {
        public async Task<string> CreatePaymentIntentAsync(decimal amount)
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = "egp",
                PaymentMethodTypes = new List<string> { "card" },
                //PaymentMethod = "pm_card_visa", 
                //Confirm = true
            };

            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }
    }
}
