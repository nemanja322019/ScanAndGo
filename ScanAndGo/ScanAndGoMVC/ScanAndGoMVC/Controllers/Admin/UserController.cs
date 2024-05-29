using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels.User;
using ServiceLibrary.Services.Interfaces;
using System.Security.Claims;

namespace ScanAndGoMVC.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var allUsersDto = await _userService.GetAll();
            return Ok(allUsersDto);
        }

        [HttpGet("get-all-store-owners")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStoreOwners()
        {
            var allUsersDto = await _userService.GetAllStoreOwners();
            return Ok(allUsersDto);
        }

        [HttpGet("get-all/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var allUsersDto = await _userService.GetAll(pageNumber, pageSize);
            return Ok(allUsersDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUser([FromBody] UpdateUserDto dto)
        {
            await _userService.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize(Roles = "Admin, StoreOwner, Seller")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            int id = int.Parse(HttpContext.User.FindFirstValue("Id"));
            await _userService.ChangePassword(id, dto);
            return Ok();
        }

    }

}


