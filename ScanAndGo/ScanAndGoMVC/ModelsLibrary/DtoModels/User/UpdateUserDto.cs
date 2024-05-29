using ModelsLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }
        public UserTypes UserType { get; set; }
        public int? WorkingInStoreId { get; set; }

    }

}
