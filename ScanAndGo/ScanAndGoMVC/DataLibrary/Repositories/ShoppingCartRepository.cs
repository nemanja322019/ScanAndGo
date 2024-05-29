using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _dbContext;
        public ShoppingCartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingCart> Add(ShoppingCart shoppingCart)
        {
            _dbContext.shoppingCart.Add(shoppingCart);
            await _dbContext.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCart?> GetByUserIdAndStoreId(int userId, int storeId)
        {
            return await _dbContext.shoppingCart
                  .Include(s => s.Items)
                  .ThenInclude(i => i.Product)
                  .FirstOrDefaultAsync(x => x.UserId == userId && x.StoreId == storeId && !x.IsExpired);
        }

        public async Task<ShoppingCart> Update(ShoppingCart shoppingCart)
        {
            _dbContext.Update(shoppingCart);
            await _dbContext.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task Delete(ShoppingCart shoppingCart)
        {
            _dbContext.shoppingCart.Remove(shoppingCart);
            await _dbContext.SaveChangesAsync();
        }

    }
}
