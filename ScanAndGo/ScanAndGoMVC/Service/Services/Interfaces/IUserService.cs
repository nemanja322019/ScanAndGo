using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Models;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Add(UpdateUserDto user);
        Task<List<UserDto>> GetAll();
        Task<PageDto<UserDto>> GetAll(int pageNumber, int pageSize);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> Update(UpdateUserDto dto);
        Task Delete(int id);
        Task<UserDto> GetUserByEmail(string email);
        Task<User> GetUserByEmailNoTrack(string email);
        Task<List<UserDto>> GetAllStoreOwners();
        Task ChangePassword(int userId, ChangePasswordDto dto);

    }
}
