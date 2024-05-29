using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Product
{
    public class ProductNotFoundException : BaseException
    {
        public  ProductNotFoundException() : base("Product not found!")
        {
        }
    }
}
