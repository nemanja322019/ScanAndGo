using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.Models;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> Add(UpdateProductDto product);
        Task<List<ProductDto>> GetAll();
        Task<PageDto<ProductDto>> GetAll(int pageNumber, int pageSize);
        Task<PageDto<ProductDto>> GetAllByStoreId(int id, int pageNumber, int pageSize);
        Task<ProductDto> GetProductById(int id);
        Task Update(UpdateProductDto dto);
        Task Delete(int id);
    }
}
