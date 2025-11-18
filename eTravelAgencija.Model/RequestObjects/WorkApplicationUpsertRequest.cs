using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;



namespace eTravelAgencija.Model.RequestObjects
{
    public class WorkApplicationUpsertRequest
    {
        public int UserId { get; set; }
        public IFormFile CvFile { get; set; }
    }
}