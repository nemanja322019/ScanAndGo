using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Exceptions.Shared;

namespace ModelsLibrary.Exceptions.Order
{

    public class OrderNotFoundException : BaseException
    {
        public OrderNotFoundException() : base("Order not found!")
        {
        }
    }
}
