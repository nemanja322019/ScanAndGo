using ModelsLibrary.Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Session> CreateStripeSession(Order order);
    }
}
