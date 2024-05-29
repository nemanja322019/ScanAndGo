using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Order
{
    public class PayOrderDto
    {
        public int UserId {  get; set; }
        public int StoreId { get; set; }
        public List<PayOrderItemDto> Items { get; set; }
    }
}
