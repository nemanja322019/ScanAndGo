using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Product
{
    public class ProductWithSameBarcodeAlreadyExistsException : BaseException
    {
        public ProductWithSameBarcodeAlreadyExistsException() : base("Product with same barcode already exists!")
        {
        }
    }
}
