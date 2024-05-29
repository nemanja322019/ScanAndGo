using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Exceptions.Shared;

namespace ModelsLibrary.Exceptions.User
{

    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base("User not found!")
        {
        }
    }
}
