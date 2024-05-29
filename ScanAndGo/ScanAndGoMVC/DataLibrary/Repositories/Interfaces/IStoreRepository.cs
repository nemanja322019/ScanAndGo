using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store> Add(Store store);
        Task<List<Store>> GetAll();
        Task<PageDto<Store>> GetAll(int pageNumber, int pageSize);
        Task Update(Store store);
        Task Delete(Store store);
        Task<Store> GetStoreById(int id);
        Task<List<Store>> GetStoresByUserId(int userId);
    }
}
