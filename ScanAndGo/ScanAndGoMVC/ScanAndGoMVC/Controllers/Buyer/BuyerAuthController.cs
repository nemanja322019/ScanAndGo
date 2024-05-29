using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DtoModels;

using ModelsLibrary.DtoModels.Auth;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.DtoModels;
using Org.BouncyCastle.Asn1.Ocsp;
using ServiceLibrary.Services;
using ServiceLibrary.Services.Interfaces;
using System.Web;

namespace ScanAndGoMVC.Controllers.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerAuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public BuyerAuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var responseDto = await _authenticationService.Login(loginUserDto);
            return Ok(responseDto);
        }

        [HttpPost("forgot-password/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var responseDto = await _authenticationService.SendResetPasswordEmail(email);
            return Ok(responseDto);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] BuyerRegisterDto buyerRegisterDto)
        {
            var responseDto = await _authenticationService.BuyerRegister(buyerRegisterDto);
            return Ok(responseDto);
        }

        [HttpPost("verification")]
        public async Task<IActionResult> Verification([FromBody] BuyerVerifyDto buyerVerifyDto)
        {
            var responseDto = await _authenticationService.BuyerVerify(buyerVerifyDto);
            return Ok(responseDto);
        }

        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerificationEmail(EmailDto email)
        {
            var responseDto = await _authenticationService.ResendVerificationCode(email.Email);
            return Ok(responseDto);
        }

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile([FromBody] CompleteProfileDto completeProfile)
        {
            var responseDto = await _authenticationService.CompleteProfile(completeProfile);
            return Ok(responseDto);
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var responseDto = await _authenticationService.BuyerSendResetPasswordEmail(email);
            return Ok(responseDto);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var responseDto = await _authenticationService.ResetPassword(dto);
            return Ok(responseDto);
        }

        [HttpGet("redirect-to-deep-link")]
        public IActionResult RedirectToDeepLink([FromQuery] string url)
        {
            Console.WriteLine("Primljen na back: " + url);
            return Redirect(url);
        }
    }
}
