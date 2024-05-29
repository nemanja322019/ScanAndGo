using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Auth;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Models;


namespace ServiceLibrary.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseDto> Login(LoginUserDto loginUserDto);
        Task<ResponseDto> Register(RegisterUserDto registerUser);
        Task<ResponseDto> BuyerRegister(BuyerRegisterDto buyerRegisterDto);
        Task<ResponseDto> BuyerVerify(BuyerVerifyDto buyerVerifyDto);
        Task<ResponseDto> ResendVerificationCode(string email);
        Task<ResponseDto> SendResetPasswordEmail(string email);
        Task<ResponseDto> BuyerSendResetPasswordEmail(string email);
        Task<ResponseDto> ResetPassword(ResetPasswordDTO dto);

        Task<ResponseDto> CompleteProfile(CompleteProfileDto dto);
    }

}
