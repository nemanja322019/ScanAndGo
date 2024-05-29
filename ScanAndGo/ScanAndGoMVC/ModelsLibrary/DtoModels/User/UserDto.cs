using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.Enums;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.User
{

    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public UserTypes UserType { get; set; }
        public virtual StoreDto? WorkingInStore { get; set; }
        public virtual ICollection<StoreDto>? OwnedStores { get; set; }
        public bool TemporalPassword { get; private set; }

    }
}
