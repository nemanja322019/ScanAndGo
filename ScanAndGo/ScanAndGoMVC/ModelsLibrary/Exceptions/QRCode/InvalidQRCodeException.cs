using ModelsLibrary.Exceptions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Exceptions.QRCode
{
    public class InvalidQRCodeException : BaseException
    {
        public  InvalidQRCodeException() : base("Invalid QR code exception!")
        {
        }
    }
}
