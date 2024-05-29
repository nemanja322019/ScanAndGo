using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class Product
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }

        public double Price { get; private set; }
        public double Weight { get; private set; }

        public int? StoreId { get; private set; }

        [ForeignKey("StoreId")]
        public  Store? Store { get; private set; }

        [StringLength(20)]
        public string? Barcode { get; private set; }

        private Product(int id, string name, double price, double weight, int? storeId, string? barcode)
        {
            Id = id;
            Name = name;
            Price = price;
            Weight = weight;
            StoreId = storeId;
            Barcode = barcode;
        }

        public void Update(string name, double price, double weight, string barcode, int? storeId)
        {
            Name = name;
            Price = price;
            Weight = weight;
            Barcode = barcode;
            StoreId = storeId;
        }

        public static Product Create(int id, string name, double price, double weight, int storeId, string barcode)
        {
            return new Product(id, name, price, weight, storeId, barcode);
        }
    }
}
