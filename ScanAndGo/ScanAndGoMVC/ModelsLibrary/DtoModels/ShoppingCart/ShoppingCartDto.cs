using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.ShoppingCart
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        //public UserDto User { get; set; }
        //public StoreDto Store { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; }
    }
}
