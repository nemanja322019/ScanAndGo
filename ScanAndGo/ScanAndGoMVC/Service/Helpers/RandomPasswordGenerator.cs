using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Helpers
{
    public static class RandomPasswordGenerator
    {
        public static string GeneratePassword()
        {
            int length = 8;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
            var randomBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            var sb = new StringBuilder(length);
            foreach (var b in randomBytes)
            {
                sb.Append(validChars[b % (validChars.Length)]);
            }
            return sb.ToString();
        }
    }
   
}
