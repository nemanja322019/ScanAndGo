using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.DtoModels.User;

namespace ModelsLibrary.DtoModels.Auth
{
    public class ResponseDto
    {
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
        public string Result { get; set; }

        public ResponseDto()
        {
            Token = "";
            UserDto = null;
            Result = "";
        }

        public ResponseDto(string result)
        {
            Token = "";
            UserDto = null;
            Result = result;
        }

        public ResponseDto(string token, UserDto korisnikDto, string result)
        {
            Token = token;
            UserDto = korisnikDto;
            Result = result;
        }
    }
}
