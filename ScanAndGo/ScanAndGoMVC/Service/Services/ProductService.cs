using AutoMapper;
using DataLibrary.Repositories;
using DataLibrary.Repositories.Interfaces;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.Exceptions.Product;
using ModelsLibrary.Exceptions.Store;
using ModelsLibrary.Models;
using ServiceLibrary.Services.Interfaces;

namespace ServiceLibrary.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Add(UpdateProductDto dto)
        {
            var existingProduct = await _productRepository.GetProductByBarcode(dto.Barcode);
            if (existingProduct != null) throw new ProductWithSameBarcodeAlreadyExistsException();
            var productToAdd = await _productRepository.Add(_mapper.Map<Product>(dto));
            return _mapper.Map<ProductDto>(productToAdd);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetProductById(id) ?? throw new ProductNotFoundException();
            await _productRepository.Delete(product);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return products.Select(_mapper.Map<ProductDto>).ToList();
        }

        public async Task<PageDto<ProductDto>> GetAll(int pageNumber, int pageSize)
        {
            var page = await _productRepository.GetAll(pageNumber, pageSize);
            PageDto<ProductDto> pageDto = new(_mapper.Map<List<ProductDto>>(page.Data), page.TotalCount);
            return pageDto;
        }

        public async Task<PageDto<ProductDto>> GetAllByStoreId(int id, int pageNumber, int pageSize)
        {
            var page = await _productRepository.GetAllByStoreId(id, pageNumber, pageSize);
            PageDto<ProductDto> pageDto = new(_mapper.Map<List<ProductDto>>(page.Data), page.TotalCount);
            return pageDto;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id) ?? throw new ProductNotFoundException();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task Update(UpdateProductDto dto)
        {
            var product = await _productRepository.GetProductById(dto.Id) ?? throw new ProductNotFoundException();
            var existingProduct = await _productRepository.GetProductByBarcode(dto.Barcode);
            if (existingProduct != null && existingProduct.Id != product.Id) throw new ProductWithSameBarcodeAlreadyExistsException();
            product.Update(dto.Name, dto.Price, dto.Weight, dto.Barcode, dto.StoreId);
            await _productRepository.Update(product);
        }
    }
}
