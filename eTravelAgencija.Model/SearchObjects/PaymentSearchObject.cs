using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class PaymentSearchObject : BaseSearchObject
    {
        public int reservationId { get; set; }

        public int rateId { get; set; }

    }
}