using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.ShoppingCart
{
    public class AddToCartDto
    {
        public int BuyerId { get; set; }
        public string Barcode { get; set; }
    }
}
