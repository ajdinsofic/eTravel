using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Model.RequestObjects
{
    public class UserImageRequest
    {
        public int userId { get; set; }
        public IFormFile Image { get; set; }
    }
}
