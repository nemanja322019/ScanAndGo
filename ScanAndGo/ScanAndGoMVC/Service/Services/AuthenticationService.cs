using AutoMapper;
using DataLibrary.Repositories.Interfaces;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Auth;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Enums;
using ModelsLibrary.Exceptions;
using ModelsLibrary.Exceptions.Auth;
using ModelsLibrary.Exceptions.Shared;
using ModelsLibrary.Exceptions.User;
using ModelsLibrary.Models;
using ServiceLibrary.Helpers;
using ServiceLibrary.Helpers.EmailBody;
using ServiceLibrary.Services.Interfaces;
using System.Security.Cryptography;


namespace ServiceLibrary.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
        }

  
        public async Task<ResponseDto> Login(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Email) && string.IsNullOrEmpty(loginUserDto.Password))
                throw new FieldsRequiredException();

            var user = await _userRepository.GetUserByEmail(loginUserDto.Email);

            if (user == null)
                throw new UserNotFoundException();

            if (user.TemporalPassword && user.ResetPasswordExpire < DateTime.Now) 
                throw new TemporaryPasswordExpiredException();

            if (user.UserType == UserTypes.Buyer && !user.IsVerified)
            {
                await _userRepository.Delete(user);
                throw new UnverifiedUserException(); 
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.Password, loginUserDto.Password);
            if (result == PasswordVerificationResult.Success)
            {
                var token = JwtTokenGenerator.GenerateNewJsonWebToken(_mapper.Map<User>(user), _configuration);
                UserDto UserDto = _mapper.Map<UserDto>(user);
                return new ResponseDto(token, UserDto, "Uspesno ste se logovali na sistem");
            }

            throw new WrongPasswordException();
        }

  
        public async Task<ResponseDto> Register(RegisterUserDto registerUser)
        {
            var existingUser = await _userRepository.GetUserByEmail(registerUser.Email);
            if (existingUser != null)
                throw new UserWithSameEmailAlreadyExistsException();

            User user = _mapper.Map<User>(registerUser);
            string password = RandomPasswordGenerator.GeneratePassword();
            user.UpdatePassword(passwordHasher.HashPassword(null, password));
            user.UpdateTemporalPassword(true);
            user.UpdateResetPasswordExpire(DateTime.Now.AddHours(24));
            await _userRepository.Add(user);

            var emailModel = new Email(user.Email, "New registration!", RegisterEmailBody.RegisterEmailStringBody(user.Email, password));
            _emailService.SendEmail(emailModel, SecureSocketOptions.Auto);

            var token = JwtTokenGenerator.GenerateNewJsonWebToken(user, _configuration);
            return new ResponseDto(token, _mapper.Map<UserDto>(user), "Uspesno ste se registrovali");
        }

        public async Task<ResponseDto> BuyerRegister(BuyerRegisterDto buyerRegisterDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(buyerRegisterDto.Email);
            if (existingUser != null)
                throw new UserWithSameEmailAlreadyExistsException();

            buyerRegisterDto.UserType = UserTypes.Buyer;

            User user = _mapper.Map<User>(buyerRegisterDto);

            string verificationCode = RandomPasswordGenerator.GeneratePassword();
            user.UpdateVerificationParams(verificationCode, DateTime.Now.AddMinutes(15), false);
            user.UpdatePassword(passwordHasher.HashPassword(null, buyerRegisterDto.Password));
            user.UpdateTemporalPassword(false);
            await _userRepository.Add(user);

            var emailModel = new Email(user.Email, "New registration!", VerifyEmailBody.VerifyEmailStringBody(user.Email, verificationCode));
            _emailService.SendEmail(emailModel, SecureSocketOptions.Auto);

            var token = JwtTokenGenerator.GenerateNewJsonWebToken(user, _configuration);
            return new ResponseDto(token, _mapper.Map<UserDto>(user), "Uspesno ste se registrovali");
        }

        public async Task<ResponseDto> ResendVerificationCode(string email)
        {
            var user = await _userRepository.GetUserByEmailNoTrack(email) ?? throw new UserNotFoundException();

            string verificationCode = RandomPasswordGenerator.GeneratePassword();
            user.UpdateVerificationParams(verificationCode, DateTime.Now.AddMinutes(15), false);
            await _userRepository.Update(user);
            var emailModel = new Email(user.Email, "New registration!", VerifyEmailBody.VerifyEmailStringBody(user.Email, verificationCode));
            _emailService.SendEmail(emailModel, SecureSocketOptions.Auto);
            return new ResponseDto("", _mapper.Map<UserDto>(user), "Verifikacioni kod poslat.");
        }

        public async Task<ResponseDto> BuyerVerify(BuyerVerifyDto buyerVerifyDto)
        {
            var user = await _userRepository.GetUserByEmailNoTrack(buyerVerifyDto.Email) ?? throw new UserNotFoundException();
            if (user.VerifyEmailExpire < DateTime.Now) throw new ResetPasswordTokenExpiredException();

            if (user.VerificationCode != buyerVerifyDto.VerificationCode) throw new InvalidVerificationCodeException();
            user.UpdateIsVerified(true);
            await _userRepository.Update(user);
            return new ResponseDto("", _mapper.Map<UserDto>(user), "Uspesno ste se verifikovali.");

        }

        public async Task<ResponseDto> CompleteProfile(CompleteProfileDto dto)
        {
            User user = await _userRepository.GetUserByEmailNoTrack(dto.Email) ?? throw new UserNotFoundException();
            user.UpdateProfile(dto.Address);
            await _userRepository.Update(user);

            return new ResponseDto("", _mapper.Map<UserDto>(user), "Profil izmenjen.");
        }

        public async Task<ResponseDto> SendResetPasswordEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email) ?? throw new UserNotFoundException();

            string password = RandomPasswordGenerator.GeneratePassword();
            user.UpdatePassword(passwordHasher.HashPassword(null, password));
            user.UpdateTemporalPassword(true);
            user.UpdateResetPasswordExpire(DateTime.Now.AddHours(24));

            var emailModel = new Email(email, "Reset password!", ResetPasswordEmailBody.ResetPasswordEmailStringBody(email, password));
            _emailService.SendEmail(emailModel, SecureSocketOptions.Auto);
            await _userRepository.Update(user);

            return new ResponseDto("", _mapper.Map<UserDto>(user), "Morate promeniti password u narednih 24h.");
        }

        public async Task<ResponseDto> BuyerSendResetPasswordEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email) ?? throw new UserNotFoundException();

            var token = RandomNumberGenerator.GetBytes(64);
            var emailtoken = Convert.ToBase64String(token);
            user.UpdateResetPasswordParams(emailtoken, DateTime.Now.AddMinutes(15));
            var emailmodel = new Email(email, "reset password!", EmailBody.EmailStringBody(email, emailtoken));
            _emailService.SendEmail(emailmodel, SecureSocketOptions.Auto);
            await _userRepository.Update(user);
            return new ResponseDto("", _mapper.Map<UserDto>(user), "Email poslat.");
        }

        public async Task<ResponseDto> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userRepository.GetUserByEmailNoTrack(dto.Email) ?? throw new UserNotFoundException();
            if (user.ResetPasswordToken != dto.EmailToken) throw new InvalidResetLinkException();
            if (user.ResetPasswordExpire < DateTime.Now) throw new ResetPasswordTokenExpiredException();

            var result = passwordHasher.VerifyHashedPassword(user, user.Password, dto.OldPassword);
            if (result == PasswordVerificationResult.Success)
            {
                user.UpdateTemporalPassword(false);
                user.UpdatePassword(passwordHasher.HashPassword(null, dto.NewPassword));
                await _userRepository.Update(user);
                return new ResponseDto("", _mapper.Map<UserDto>(user), "Uspesno ste se izmenili sifru.");
            }

            throw new WrongPasswordException();

        }

    }
}
