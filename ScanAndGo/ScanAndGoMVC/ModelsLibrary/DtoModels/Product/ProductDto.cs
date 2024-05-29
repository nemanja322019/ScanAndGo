using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ModelsLibrary.DtoModels.Store;

namespace ModelsLibrary.DtoModels.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public double Weight { get; set; }
        public string Barcode { get; set; }
        public StoreDto? Store { get; set; }
    }
}
