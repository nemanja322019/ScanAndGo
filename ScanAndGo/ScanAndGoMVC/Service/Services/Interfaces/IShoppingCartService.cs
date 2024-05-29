using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Order;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.DtoModels.ShoppingCart;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetByUserIdAndStoreId(int userId, int storeId);
        Task<ProductDto> AddToCart(int userId, string barcode);
        Task RemoveFromCart(int userId, int productId);
        Task IncreaseQuantity(int userId, int productId);
        Task DecreaseQuantity(int userId, int productId);
        Task RemoveItemsFromCart(int userId, PayOrderDto dto);
    }
}
