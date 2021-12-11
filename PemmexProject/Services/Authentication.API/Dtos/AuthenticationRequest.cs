using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Dtos
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
