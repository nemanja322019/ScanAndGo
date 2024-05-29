using Microsoft.Extensions.Configuration;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Session> CreateStripeSession(Order order)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = _configuration["PaymentSession:SuccessUrl"] + order.Id,
                CancelUrl = _configuration["PaymentSession:CancelUrl"],
                LineItems = [],
                Mode = "payment",
            };

            foreach (var item in order.Items)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.ProductPrice * 100), //convert to cents
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName,
                        }
                    },
                    Quantity = item.ProductCount
                };

                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            return service.Create(options);
        }
      
    }
}
