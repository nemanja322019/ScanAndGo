using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Order
{
    public class OrderForExcelDto
    {
        public string Store { get; set; }

        public string User { get; set; }
        public string? PaymentStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? SessionId { get; set; }
    }
}
