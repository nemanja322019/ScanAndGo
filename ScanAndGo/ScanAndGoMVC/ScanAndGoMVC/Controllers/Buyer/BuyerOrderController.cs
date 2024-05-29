using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Services.Interfaces;
using Stripe;
using System.Security.Claims;
using Stripe.Checkout;
using ModelsLibrary.DtoModels.Order;

namespace ScanAndGoMVC.Controllers.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public BuyerOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBuyerOrders()
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var orders = await _orderService.GetOrdersByUserId(int.Parse(id));
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost("pay-order")]
        [Authorize]
        public async Task<IActionResult> PayOrder([FromBody] PayOrderDto dto)
        {
            Session session = await _orderService.Pay(dto);
            return Ok(new { url = session.Url });
        }

        [HttpGet("paymentConfirmation/{orderId}")]
        [Authorize]
        public async Task<IActionResult> PaymentConfirmation(int orderId)
        {
            var qrCode = await _orderService.PaymentConfirmation(orderId);
            return Ok(qrCode);
        }

        [HttpGet("weight/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderWeight(int orderId)
        {
            var weight = await _orderService.CalculateOrderWeight(orderId);
            return Ok(weight);
        }
    }
}
