using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels.Auth;
using ServiceLibrary.Services.Interfaces;
using System.Security.Claims;


namespace ScanAndGoMVC.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var responseDto = await _authenticationService.Login(loginUserDto);
            return Ok(responseDto);
        }

        [HttpPost("registration")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserDto registerUser)
        {
            var responseDto = await _authenticationService.Register(registerUser);
            return Ok(responseDto);
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            await _authenticationService.SendResetPasswordEmail(email);
            return Ok();
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.GetUserByEmail(email);
            return Ok(user);
        }

    }

}


