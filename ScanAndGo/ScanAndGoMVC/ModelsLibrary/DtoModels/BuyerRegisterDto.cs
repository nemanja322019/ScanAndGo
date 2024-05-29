using ModelsLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels
{
    public class BuyerRegisterDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserTypes UserType { get; set; }
    }
}
