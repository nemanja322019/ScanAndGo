using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Exceptions.Shared;

namespace ModelsLibrary.Exceptions.User
{
    public class UserWithSameEmailAlreadyExistsException : BaseException
    {
        public UserWithSameEmailAlreadyExistsException() : base("User with same email already exists!")
        {
        }
    }
}
