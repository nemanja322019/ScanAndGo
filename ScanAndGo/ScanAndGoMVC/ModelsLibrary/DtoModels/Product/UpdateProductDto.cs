using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public double Weight { get; set; }
        public string Barcode { get; set; }

        public int? StoreId { get; set; }

    }
}
