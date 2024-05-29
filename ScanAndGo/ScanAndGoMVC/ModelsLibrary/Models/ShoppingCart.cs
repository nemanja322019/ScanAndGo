using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class ShoppingCart
    {
        public int Id { get; private set; }
        public int UserId { get; private set; } 
        public User User { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; } = new();
        public bool IsExpired {  get; private set; }
        public DateTime ExpirationTime { get; private set; }
        private ShoppingCart(int userId, int storeId)
        {
            UserId = userId;
            StoreId = storeId;
            IsExpired = false;
            ExpirationTime = DateTime.Now.AddHours(24);
        }
        public static ShoppingCart Create(int userId, int storeId)
        {
            return new ShoppingCart(userId, storeId);
        }

        public void AddItem(ShoppingCartItem item)
        {
            Items.Add(item);
        }

        public void SetExpired(bool isExpired)
        {
            IsExpired = isExpired;
        }
    }
}
