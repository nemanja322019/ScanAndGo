using ModelsLibrary.DtoModels.ShoppingCart;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }

        public string UserEmail { get; set; }

        public IEnumerable<OrderItemDto> Items { get; set; }

        public string? PaymentStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? SessionId { get; set; }

        public string? QRCodeURL { get; set; }
    }
}
