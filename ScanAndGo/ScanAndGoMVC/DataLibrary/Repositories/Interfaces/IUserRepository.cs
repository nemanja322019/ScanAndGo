using ModelsLibrary.DtoModels;
using ModelsLibrary.Enums;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<List<User>> GetAll();
        Task<PageDto<User>> GetAll(int pageNumber, int pageSize);
        Task<User> GetUserById(int id);
        Task<User> Update(User user);
        Task Delete(User user);
        Task SaveChanges();
        Task<User?> GetUserByEmail(string email);
        Task<User> GetUserByEmailNoTrack(string email);
        void Save(User user);
        Task<List<User>> GetAllByUserType(UserTypes type);
    }
}
