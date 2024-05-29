using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.Models;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IStoreService
    {
        Task<StoreDto> Add(UpdateStoreDto dto);
        Task<PageDto<StoreDto>> GetAll(int pageNumber, int pageSize);
        Task<List<StoreDto>> GetAll();
        Task<List<BuyerStoreDto>> BuyerGetAll();
        Task<List<BuyerStoreLocationDto>> BuyerGetAllByLocation(BuyerLocationDto buyerLocationDto);
        Task Update(UpdateStoreDto dto);
        Task Delete(int id);
        Task<StoreDto> GetStoreById(int id);
        Task<List<StoreDto>> GetStoresByUserId(int userId);
    }
}
