using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Services
{
    public interface IImageEntity
    {
        int ReferenceId { get; set; }   
        string ImageUrl { get; set; }  
    }
}