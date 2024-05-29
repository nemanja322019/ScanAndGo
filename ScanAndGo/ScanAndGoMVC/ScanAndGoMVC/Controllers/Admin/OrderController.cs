using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;
using ServiceLibrary.Helpers;
using ServiceLibrary.Services.Interfaces;
using Stripe.Checkout;
using System.Security.Claims;

namespace ScanAndGoMVC.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _orderService { get; set; }

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all/{pageNumber}/{pageSize}/{filter?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders(int pageNumber, int pageSize, string filter = "")
        {
            var orders = await _orderService.GetAll(pageNumber, pageSize, filter);
            return Ok(orders);
        }

        [HttpGet("get-all/store/{storeId}/{pageNumber}/{pageSize}/{filter?}")]
        [Authorize(Roles = "StoreOwner")]
        public async Task<IActionResult> GetAllByStoreId(int storeId, int pageNumber, int pageSize, string filter = "")
        {
            var orders = await _orderService.GetAllByStoreId(storeId, pageNumber, pageSize, filter);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, StoreOwner")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, StoreOwner")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.Delete(id);
            return Ok();
        }

        [HttpGet("download")]
        [Authorize(Roles = "Admin")]
        public async Task<FileResult> Download()
        {
            var file = await _orderService.CreateAllOrdersFile();
            return File(file, "application/octet-stream", "orders.xlsx");
        }
    }
}
