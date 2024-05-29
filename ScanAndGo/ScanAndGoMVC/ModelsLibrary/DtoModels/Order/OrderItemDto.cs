using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ProductName { get;  set; }
        public double ProductPrice { get; set; }
        public double ProductWeight { get; set; }
        public int ProductCount { get; set; }

    }
}
