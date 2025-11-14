using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObject
{
    public class DestinationStatsResponse
    {
        public string DestinationName { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

}