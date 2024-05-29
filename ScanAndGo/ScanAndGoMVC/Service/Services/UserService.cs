using AutoMapper;
using DataLibrary.Repositories;
using DataLibrary.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Enums;
using ModelsLibrary.Exceptions;
using ModelsLibrary.Exceptions.Auth;
using ModelsLibrary.Exceptions.User;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;

namespace ServiceLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<UserDto> Add(UpdateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            user.UpdatePassword(_passwordHasher.HashPassword(null, dto.Password));
            var userToAdd = await _userRepository.Add(user);
            return _mapper.Map<UserDto>(userToAdd);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetUserById(id) ?? throw new UserNotFoundException();
            await _userRepository.Delete(user);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.Select(_mapper.Map<UserDto>).ToList();
        }

        public async Task<PageDto<UserDto>> GetAll(int pageNumber, int pageSize)
        {
            var page = await _userRepository.GetAll(pageNumber, pageSize);
            PageDto<UserDto> pageDto = new(_mapper.Map<List<UserDto>>(page.Data), page.TotalCount);
            return pageDto;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetUserByEmail(email));
        }

        public async Task<User> GetUserByEmailNoTrack(string email)
        {
            return await _userRepository.GetUserByEmailNoTrack(email);
        }
        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id) ?? throw new UserNotFoundException();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Update(UpdateUserDto dto)
        {
            var user = await _userRepository.GetUserById(dto.Id) ?? throw new UserNotFoundException();

            var userWithSameEmail = await _userRepository.GetUserByEmail(dto.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != dto.Id) throw new UserWithSameEmailAlreadyExistsException();

            user.Update(dto.Name, dto.Email, dto.UserType, dto.WorkingInStoreId);
            if (dto.Password != null) user.UpdatePassword(_passwordHasher.HashPassword(null, dto.Password));

            var updatedUser = await _userRepository.Update(user);
            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<List<UserDto>> GetAllStoreOwners()
        {
            var users = await _userRepository.GetAllByUserType(UserTypes.StoreOwner);
            return users.Select(_mapper.Map<UserDto>).ToList();
        }

        public async Task ChangePassword(int userId, ChangePasswordDto dto)
        {
            var user = await _userRepository.GetUserById(userId) ?? throw new UserNotFoundException();
            if (user.TemporalPassword && user.ResetPasswordExpire < DateTime.Now) throw new TemporaryPasswordExpiredException();

            user.UpdatePassword(_passwordHasher.HashPassword(null, dto.NewPassword));
            user.UpdateTemporalPassword(false);
            await _userRepository.Update(user);
        }
    }
}


