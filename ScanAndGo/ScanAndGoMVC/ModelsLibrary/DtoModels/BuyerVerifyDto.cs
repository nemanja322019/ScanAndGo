﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels
{
    public class BuyerVerifyDto
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
