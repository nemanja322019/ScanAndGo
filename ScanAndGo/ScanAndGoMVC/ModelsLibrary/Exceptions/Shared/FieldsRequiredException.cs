using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Shared
{
    public class FieldsRequiredException : BaseException
    {
        public FieldsRequiredException() : base("Fields cannot be empty!")
        {
        }
    }
}
