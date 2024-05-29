using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelsLibrary.DtoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsLibrary.Models
{
    public class Store
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public string Name { get; private set; }

        public string Address { get; private set; }

        [ValidateNever]
        public virtual ICollection<Product>? Products { get; private set; }
        public int? UserId { get; private set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; private set; }
        public virtual ICollection<User>? Sellers { get; private set; }

        public virtual ICollection<Order>? Orders { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        [NotMapped]
        public double Distance { get; set; }

        private Store(int id, string name, string address, int? userId)
        {
            Id = id;
            Name = name;
            Address = address;
            UserId = userId;
        }

        public static Store Create(int id, string name, string address, int? userId)
        {
            return new Store(id, name, address, userId);
        }

        public void Update(string name, string address, int userId)
        {
            Name = name;
            Address = address;
            UserId = userId;
        }
    }
}
