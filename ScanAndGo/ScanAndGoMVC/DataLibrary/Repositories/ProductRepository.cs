using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(Product newProduct)
        {
            _dbContext.products.Add(newProduct);
            await _dbContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task Delete(Product product)
        {
            _dbContext.products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            var productsFromDb = await _dbContext.products
                .Include(x => x.Store)
                .ToListAsync();
            return productsFromDb;
        }

        public async Task<PageDto<Product>> GetAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.products.Include(x => x.Store).AsQueryable();
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageDto<Product>(data, totalItems);

        }
        public async Task<PageDto<Product>> GetAllByStoreId(int id, int pageNumber, int pageSize)
        {
            var query = _dbContext.products.Where(x => x.StoreId == id).AsQueryable();
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageDto<Product>(data, totalItems);

        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _dbContext.products
                .Include(x => x.Store)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetProductByBarcode(string barcode)
        {
            return await _dbContext.products
                .Include(x => x.Store)
                .FirstOrDefaultAsync(x => x.Barcode == barcode);
        }

        public async Task Update(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
