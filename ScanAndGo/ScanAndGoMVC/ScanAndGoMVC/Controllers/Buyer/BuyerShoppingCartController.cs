using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.ShoppingCart;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ScanAndGoMVC.Controllers.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public BuyerShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("get_shopping_cart")]
        [Authorize]
        public async Task<IActionResult> GetUserShoppingCartInStore([FromBody] BuyerIdStoreIdDto buyerIdStoreIdDto)
        {
            var shoppingCart = await _shoppingCartService.GetByUserIdAndStoreId(buyerIdStoreIdDto.BuyerId, buyerIdStoreIdDto.StoreId);
            return Ok(shoppingCart);
        }

        [HttpPost("add-to-cart")]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var product = await _shoppingCartService.AddToCart(addToCartDto.BuyerId, addToCartDto.Barcode);
            return Ok(product);
        }

        [HttpDelete("remove-from-cart")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int BuyerId, int ProductId)
        {
            await _shoppingCartService.RemoveFromCart(BuyerId, ProductId);
            return Ok();
        }

        [HttpPut("increase-quantity")]
        [Authorize]
        public async Task<IActionResult> IncreaseQuantity([FromBody] ManageItemDto manageItemDto)
        {
            await _shoppingCartService.IncreaseQuantity(manageItemDto.BuyerId, manageItemDto.ProductId);
            return Ok();
        }

        [HttpPut("decrease-quantity")]
        [Authorize]
        public async Task<IActionResult> DecreaseQuantity([FromBody] ManageItemDto manageItemDto)
        {
            await _shoppingCartService.DecreaseQuantity(manageItemDto.BuyerId, manageItemDto.ProductId);
            return Ok();
        }
    }
}
