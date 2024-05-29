using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Auth
{
    public class ResetPasswordTokenExpiredException : BaseException
    {
        public ResetPasswordTokenExpiredException() : base("Password expired!")
        {
        }
    }
}
