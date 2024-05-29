using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.ShoppingCart
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
