using AutoMapper;
using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Order;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.DtoModels.ShoppingCart;
using ModelsLibrary.Exceptions.Product;
using ModelsLibrary.Exceptions.ShoppingCart;
using ModelsLibrary.Exceptions.Store;
using ModelsLibrary.Exceptions.User;
using ModelsLibrary.Models;
using NPOI.XWPF.UserModel;
using ServiceLibrary.Helpers;
using ServiceLibrary.Services.Interfaces;
using System.Runtime.CompilerServices;
using System.Web;


namespace ServiceLibrary.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDto> GetByUserIdAndStoreId(int userId, int storeId)
        {
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, storeId);
            if (shoppingCart == null) shoppingCart = await _shoppingCartRepository.Add(ShoppingCart.Create(userId, storeId));
            if (shoppingCart.ExpirationTime < DateTime.Now)
            {
                shoppingCart.SetExpired(true);
                await _shoppingCartRepository.Update(shoppingCart);
                return await GetByUserIdAndStoreId(userId, storeId);
                 
            }

            return _mapper.Map<ShoppingCartDto>(shoppingCart);
        }


        public async Task<ProductDto> AddToCart(int userId, string barcode)
        {
            var product = await _productRepository.GetProductByBarcode(barcode) ?? throw new ProductNotFoundException();
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, product.StoreId ?? -1);
            if (shoppingCart == null) shoppingCart = await _shoppingCartRepository.Add(ShoppingCart.Create(userId, product.StoreId ?? -1));
            if (shoppingCart.ExpirationTime < DateTime.Now)
            {
                shoppingCart.SetExpired(true);
                await _shoppingCartRepository.Update(shoppingCart);
                await AddToCart(userId, barcode);
            }

            var existingItemIndex = shoppingCart.Items.FindIndex(item => item.ProductId == product.Id);
            if (existingItemIndex != -1)
            {
                shoppingCart.Items[existingItemIndex].IncreaseQuantity();
            }
            else
            {
                shoppingCart.AddItem(ShoppingCartItem.Create(product.Id, 1));
            }
            await _shoppingCartRepository.Update(shoppingCart);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task RemoveFromCart(int userId, int productId)
        {
            var product = await _productRepository.GetProductById(productId) ?? throw new ProductNotFoundException();
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, product.StoreId ?? -1) ?? throw new ShoppingCartNotFoundException();
           
            var existingItemIndex = shoppingCart.Items.FindIndex(item => item.ProductId == productId);
            if (existingItemIndex != -1)
            {
                shoppingCart.Items.RemoveAt(existingItemIndex);
            }
            else
            {
                throw new ShoppingCartItemNotFoundException();
            }
            await _shoppingCartRepository.Update(shoppingCart);
        }

        public async Task IncreaseQuantity(int userId, int productId)
        {
            var product = await _productRepository.GetProductById(productId) ?? throw new ProductNotFoundException();
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, product.StoreId ?? -1) ?? throw new ShoppingCartNotFoundException();
            ;
            var existingItemIndex = shoppingCart.Items.FindIndex(item => item.ProductId == productId);
            if (existingItemIndex != -1)
            {
                shoppingCart.Items[existingItemIndex].IncreaseQuantity();
            }
            else
            {
                throw new ShoppingCartItemNotFoundException();
            }
            await _shoppingCartRepository.Update(shoppingCart);
        }

        public async Task DecreaseQuantity(int userId, int productId)
        {
            var product = await _productRepository.GetProductById(productId) ?? throw new ProductNotFoundException();
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, product.StoreId ?? -1) ?? throw new ShoppingCartNotFoundException();
            ;
            var existingItemIndex = shoppingCart.Items.FindIndex(item => item.ProductId == productId);
            if (existingItemIndex != -1)
            {
                if (shoppingCart.Items[existingItemIndex].Quantity == 1) shoppingCart.Items.RemoveAt(existingItemIndex);
                else shoppingCart.Items[existingItemIndex].DecreaseQuantity();
            }
            else
            {
                throw new ShoppingCartItemNotFoundException();
            }
            await _shoppingCartRepository.Update(shoppingCart);
        }

        public async Task RemoveItemsFromCart(int userId, PayOrderDto dto)
        {
            var shoppingCart = await _shoppingCartRepository.GetByUserIdAndStoreId(userId, dto.StoreId) ?? throw new ShoppingCartNotFoundException();

            foreach (PayOrderItemDto orderItem in dto.Items)
            {
                var existingItemIndex = shoppingCart.Items.FindIndex(item => item.ProductId == orderItem.ProductId);
                if (existingItemIndex != -1)
                {
                    shoppingCart.Items.RemoveAt(existingItemIndex);
                }
                else
                {
                    throw new ShoppingCartItemNotFoundException();
                }
            }
            if (shoppingCart.Items.Count == 0) 
                await _shoppingCartRepository.Delete(shoppingCart);
            else
                await _shoppingCartRepository.Update(shoppingCart);
        }

    }
}
