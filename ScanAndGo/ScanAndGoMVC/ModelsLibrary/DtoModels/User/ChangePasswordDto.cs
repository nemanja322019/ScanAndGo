using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels.User
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
