using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _dbContext;
        public StoreRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<Store> Add(Store store)
        {
            _dbContext.stores.Add(store);
            await _dbContext.SaveChangesAsync();
            return store;
        }

        public async Task Delete(Store store)
        {  
            _dbContext.stores.Remove(store);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PageDto<Store>> GetAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.stores.Include(x => x.User)
                        .AsQueryable();
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageDto<Store>(data, totalItems);

        }

        public async Task<List<Store>> GetAll()
        {
            return await _dbContext.stores
               .Include(x => x.User)
               .ToListAsync();
        }

        public async Task<Store?> GetStoreById(int id)
        {
            return await _dbContext.stores
                .Include(x => x.Sellers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Store>> GetStoresByUserId(int userId)
        {
            var stores = await _dbContext.stores
                .Where(a => a.UserId == userId)
                .Include(x => x.Sellers)
                .ToListAsync();
            return stores;
        }

        public async Task Update(Store store)
        {
            _dbContext.Update(store);
            await _dbContext.SaveChangesAsync();
        }
    }
}
