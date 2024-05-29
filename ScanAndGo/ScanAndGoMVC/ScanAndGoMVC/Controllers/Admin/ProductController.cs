using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;

namespace ScanAndGoMVC.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("get-all/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var products = await _productService.GetAll(pageNumber, pageSize);
            return Ok(products);
        }

        [HttpGet("get-all-by-store-id/{id}/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Admin, StoreOwner, Seller")]
        public async Task<IActionResult> GetAllByStoreId(int id, int pageNumber, int pageSize)
        {
            var products = await _productService.GetAllByStoreId(id, pageNumber, pageSize);
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, StoreOwner, Seller")]
        public async Task<IActionResult> CreateProduct([FromBody] UpdateProductDto dto)
        {
            return Ok(await _productService.Add(dto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin, StoreOwner, Seller")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto dto)
        {
            await _productService.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, StoreOwner, Seller")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}
