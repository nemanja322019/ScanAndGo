using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Auth
{
    public class UnverifiedUserException : BaseException
    {
        public UnverifiedUserException() : base("Unverified account deleted, please register and verify your account.")
        {

        }
    }
}
