using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class VoucherUpsertRequest
    {
        public string VoucherCode { get; set; }
        public decimal Discount { get; set; }
        public int priceInTokens { get; set; }
    }
}