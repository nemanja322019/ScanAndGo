using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public double ProductPrice { get; private set; }
        public double ProductWeight { get; private set; }
        public int ProductCount { get; private set; }
        public int? OrderId {  get; private set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; private set; }

        private OrderItem(string productName, double productPrice, double productWeight, int productCount)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductWeight = productWeight;
            ProductCount = productCount;
        }

        public static OrderItem Create(string productName, double productPrice, double productWeight, int productCount)
        {
            return new OrderItem(productName, productPrice, productWeight, productCount);
        }
    }
}
