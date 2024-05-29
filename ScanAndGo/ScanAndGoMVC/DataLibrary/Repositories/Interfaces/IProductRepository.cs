using ModelsLibrary.DtoModels;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Add(Product newProduct);
        Task<List<Product>> GetAll();
        Task<PageDto<Product>> GetAll(int pageNumber, int pageSize);
        Task<PageDto<Product>> GetAllByStoreId(int id, int pageNumber, int pageSize);
        Task<Product?> GetProductById(int id);
        Task<Product?> GetProductByBarcode(string barcode);
        Task Update(Product dto);
        Task Delete(Product product);
    }
}
