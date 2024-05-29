using ModelsLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class Order
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }
        [ForeignKey("UserId")]
        public User? User { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }

        public IEnumerable<OrderItem> Items => _items;
        private List<OrderItem> _items = new List<OrderItem>();

        public PaymentStatus PaymentStatus { get; private set; }

        public DateTime? PaymentDate { get; private set; }

        public string? SessionId { get; private set; }
        public string? QRCodeURL { get; private set; }

        private Order(int userId, int storeId, PaymentStatus paymentStatus)
        {
            UserId = userId;
            StoreId = storeId;
            PaymentStatus = paymentStatus;
        }

        public static Order Create(int userId, int storeId, PaymentStatus paymentStatus)
        {
            return new Order(userId, storeId, paymentStatus);
        }

        public void UpdateSessionId(string sessionId)
        {
            SessionId = sessionId;
        }

        public void UpdatePaymentParams(PaymentStatus paymentStatus, DateTime paymentDate)
        {
            PaymentStatus = paymentStatus;
            PaymentDate = paymentDate;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
        }

        public void UpdateQRCodeURL(string qrCodeURL)
        {
            QRCodeURL = qrCodeURL;
        } 
    }
}
