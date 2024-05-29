using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Exceptions.Shared;

namespace ModelsLibrary.Exceptions.User
{
    public  class WrongPasswordException : BaseException
    {
        public WrongPasswordException() : base("Wrong password!")
        {
        }
    }
}
