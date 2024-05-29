using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> Add(ShoppingCart shoppingCart);
        Task<ShoppingCart?> GetByUserIdAndStoreId(int userId, int storeId);
        Task<ShoppingCart> Update(ShoppingCart shoppingCart);
        Task Delete(ShoppingCart shoppingCart);
        }
}
