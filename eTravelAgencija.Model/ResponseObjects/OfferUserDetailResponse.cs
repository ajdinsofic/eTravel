using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferUserDetailResponse : BaseOfferDetailResponse
    {
        public int minimalPrice { get; set; } = 0; // radi cijene koja treba da se kalkulise kasnije,
        // cijena je inace 0 kada se vraca samo deskripcija za usera 
    }
}