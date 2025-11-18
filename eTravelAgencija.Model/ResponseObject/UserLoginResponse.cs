using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObject
{
    public class UserLoginResponse
    {
        public int Userid {get; set;}
        public string username {get; set;}
        public string Token {get; set;}
        public List<string> Roles { get; set; } = new List<string>();
    }
}