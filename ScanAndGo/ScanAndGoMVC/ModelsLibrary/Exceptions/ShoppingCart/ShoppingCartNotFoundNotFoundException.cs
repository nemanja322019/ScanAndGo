using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Exceptions.Shared;

namespace ModelsLibrary.Exceptions.ShoppingCart
{

    public class ShoppingCartNotFoundException : BaseException
    {
        public ShoppingCartNotFoundException() : base("Shopping cart not found!")
        {
        }
    }
}
