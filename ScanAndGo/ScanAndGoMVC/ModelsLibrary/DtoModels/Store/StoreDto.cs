using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Store
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public UserDto? User { get; set; }
        public List<UserDto>? Sellers { get; set; }
    }
}
