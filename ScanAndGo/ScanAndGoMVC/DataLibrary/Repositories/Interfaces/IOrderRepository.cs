using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order newOrder);
        Task<PageDto<Order>> GetAll(int pageNumber, int pageSize, string filter);
        Task<PageDto<Order>> GetAllByStoreId(int storeId, int pageNumber, int pageSize, string filter);
        Task<List<Order>> GetAll();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByUserId(int id);
        Task Update(Order order);
        Task Delete(Order order);
    }
}
