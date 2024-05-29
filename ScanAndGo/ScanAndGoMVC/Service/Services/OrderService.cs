using AutoMapper;
using DataLibrary.Repositories.Interfaces;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Order;
using ModelsLibrary.Enums;
using ModelsLibrary.Exceptions.Order;
using ModelsLibrary.Models;
using ServiceLibrary.Helpers;
using ServiceLibrary.Services.Interfaces;
using Stripe.Checkout;

namespace ServiceLibrary.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IImageService _imageService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IShoppingCartService shoppingCartService, IImageService imageService, IPaymentService paymentService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shoppingCartService = shoppingCartService;
            _imageService = imageService;
            _paymentService = paymentService;
            _mapper = mapper;
        }
        public async Task<OrderDto> AddOrder(Order newOrder)
        {
            var addedOrder = await _orderRepository.AddOrder(newOrder);
            return _mapper.Map<OrderDto>(addedOrder);
        }

        public async Task Delete(int id)
        {
            var order = await _orderRepository.GetOrderById(id) ?? throw new OrderNotFoundException();
            await _orderRepository.Delete(order);
        }

        public async Task<PageDto<OrderDto>> GetAll(int pageNumber, int pageSize, string filter)
        {
           var page = await _orderRepository.GetAll(pageNumber, pageSize, filter);
           PageDto<OrderDto> pageDto = new(_mapper.Map<List<OrderDto>>(page.Data), page.TotalCount);
           return pageDto;
        }

        public async Task<PageDto<OrderDto>> GetAllByStoreId(int storeId, int pageNumber, int pageSize, string filter)
        {
            var page = await _orderRepository.GetAllByStoreId(storeId, pageNumber, pageSize, filter);
            PageDto<OrderDto> pageDto = new(_mapper.Map<List<OrderDto>>(page.Data), page.TotalCount);
            return pageDto;
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id) ?? throw new OrderNotFoundException();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetOrdersByUserId(int id)
        {
            var existingOrder = await _orderRepository.GetOrdersByUserId(id);
            return _mapper.Map<List<OrderDto>>(existingOrder);
        }

        public async Task<byte[]> CreateAllOrdersFile()
        {
            var orders = await _orderRepository.GetAll();
            return ExcelHelper.CreateFile(_mapper.Map<List<OrderForExcelDto>>(orders));
        }

        public async Task<Session> Pay(PayOrderDto dto)
        {
            var order = await CreateOrder(dto.UserId, dto);
            await _shoppingCartService.RemoveItemsFromCart(dto.UserId, dto);
            var session = await _paymentService.CreateStripeSession(order);
            order.UpdateSessionId(session.Id);
            await _orderRepository.Update(order);
            return session;
        }

        private async Task<Order> CreateOrder(int userId, PayOrderDto dto)
        {
            Order order = Order.Create(userId, dto.StoreId, PaymentStatus.NotPaid);
            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetProductById(item.ProductId);
                OrderItem orderItem = OrderItem.Create(product.Name, product.Price, product.Weight, item.Count);
                order.AddOrderItem(orderItem);
            }
            var createdOrder = await _orderRepository.AddOrder(order);
            return createdOrder;
        }

        public async Task<string> PaymentConfirmation(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId) ?? throw new OrderNotFoundException();
            order.UpdatePaymentParams (PaymentStatus.Paid, DateTime.Now);

            var weight = await CalculateOrderWeight(order.Id);
            var qrCode = QRCodeHelper.GenerateQRCodeBase64($"{weight}");

            byte[] bytes = Convert.FromBase64String(qrCode);
            var ms = new MemoryStream(bytes);
            var url = await _imageService.UploadImage(ms);
            order.UpdateQRCodeURL(url);

            await _orderRepository.Update(order);

            return url;
        } 

        public async Task<double> CalculateOrderWeight(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId) ?? throw new OrderNotFoundException();
            double totalWeight = 0;
            foreach (var item in order.Items) totalWeight += item.ProductWeight * item.ProductCount;
            return totalWeight;
        }
    }
}
