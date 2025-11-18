using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObject
{
    public class CVDownloadResponse
    {
        public byte[] fileBytes {get; set;}
        public string CvFileName {get; set;}
    }
}