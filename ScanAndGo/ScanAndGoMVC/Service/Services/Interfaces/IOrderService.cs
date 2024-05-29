using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Order;
using ModelsLibrary.Models;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(Order newProductDto);
        Task<PageDto<OrderDto>> GetAll(int pageNumber, int pageSize, string filter);
        Task<PageDto<OrderDto>> GetAllByStoreId(int storeId, int pageNumber, int pageSize, string filter);
        Task<OrderDto> GetOrderById(int id);
        Task<List<OrderDto>> GetOrdersByUserId(int id);
        Task Delete(int id);
        Task<byte[]> CreateAllOrdersFile();
        Task<Session> Pay(PayOrderDto dto);
        Task<string> PaymentConfirmation(int orderId);
        Task<double> CalculateOrderWeight(int orderId);
    }
}
