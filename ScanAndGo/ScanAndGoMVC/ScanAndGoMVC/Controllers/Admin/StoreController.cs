using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;
using System.Security.Claims;

namespace ScanAndGoMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("get-all/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var stores = await _storeService.GetAll(pageNumber, pageSize);
            return Ok(stores);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeService.GetAll();
            return Ok(stores);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateStore([FromBody] UpdateStoreDto dto)
        {
             return Ok(await _storeService.Add(dto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreDto dto)
        {
            await _storeService.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeService.Delete(id);
            return Ok();
        }

        [HttpGet("store-owner")]
        [Authorize(Roles = "StoreOwner")]
        public async Task<IActionResult> GetStoresByStoreOwner()
        {
            int id = int.Parse(HttpContext.User.FindFirstValue("Id"));
            List<StoreDto> stores = await _storeService.GetStoresByUserId(id);
            return Ok(stores);   
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, StoreOwner")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            StoreDto store = await _storeService.GetStoreById(id);
            return Ok(store);
        }

    }
}
