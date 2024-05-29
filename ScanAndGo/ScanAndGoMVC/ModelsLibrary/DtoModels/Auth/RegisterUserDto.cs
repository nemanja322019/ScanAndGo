using ModelsLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.Auth
{

    public class RegisterUserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        //public string Password { get; set; }
        public UserTypes UserType { get; set; }
        public int? WorkingInStoreId { get; set; }

    }
}
