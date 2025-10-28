using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class BaseImageResponse
    {

        public int Id { get; set; }
        public int referenceId { get; set; }
        public string ImageUrl { get; set; }
        
    }
}