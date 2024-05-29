using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public int ShoppingCartId { get; private set; }
        public ShoppingCart ShoppingCart { get; private set; }

        private ShoppingCartItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public static ShoppingCartItem Create(int productId, int quantity)
        {
            return new ShoppingCartItem(productId, quantity);
        }

        public void IncreaseQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            Quantity--;
        }
    }
}
