using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.Store
{
    public class StoreNotFoundException : BaseException
    {
        public StoreNotFoundException() : base("Store not found!")
        {
        }
    }
}
