using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            _dbContext.orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task Delete(Order order)
        {
            _dbContext.orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PageDto<Order>> GetAll(int pageNumber, int pageSize, string filter)
        {
            var query = _dbContext.orders.Include(o => o.User).Include(x => x.Store).AsQueryable();
            if (!string.IsNullOrEmpty(filter)) query = query.Where(o => o.SessionId.ToLower().StartsWith(filter.ToLower()) || o.User.Email.ToLower().StartsWith(filter.ToLower()));
            var totalItems = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PageDto<Order>(data, totalItems);
        }

        public async Task<PageDto<Order>> GetAllByStoreId(int storeId, int pageNumber, int pageSize, string filter)
        {
            var query = _dbContext.orders.Include(o => o.User).Where(x => x.StoreId == storeId).AsQueryable();
            if (!string.IsNullOrEmpty(filter)) query = query.Where(o => o.SessionId.ToLower().StartsWith(filter.ToLower()) || o.User.Email.ToLower().StartsWith(filter.ToLower()));
            var totalItems = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PageDto<Order>(data, totalItems);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _dbContext.orders.
                    Include(o => o.User)
                    .Include(o => o.Store)
                    .ToListAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _dbContext.orders
                .Include(x => x.Store)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetOrdersByUserId(int id)
        {
            return await _dbContext.orders
                .Include(o => o.Store)
                .Where(o => o.UserId == id).ToListAsync();
        }

        public async Task Update(Order order)
        {
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();

        }
    }
}
