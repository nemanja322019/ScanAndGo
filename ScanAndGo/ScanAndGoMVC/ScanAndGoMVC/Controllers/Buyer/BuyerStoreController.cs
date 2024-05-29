using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Auth;
using ServiceLibrary.Services.Interfaces;

namespace ScanAndGoMVC.Controllers.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerStoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public BuyerStoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("buyer-get-all-stores")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeService.BuyerGetAll();
            return Ok(stores);
        }

        [HttpPost("get-stores-by-location")]
        [Authorize]
        public async Task<IActionResult> GetAllByLocation([FromBody] BuyerLocationDto buyerLocationDto)
        {
            var stores = await _storeService.BuyerGetAllByLocation(buyerLocationDto);
            return Ok(stores);

        }

    }
}
