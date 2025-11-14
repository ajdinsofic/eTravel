using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public class Comment
    {
        public int Id {get; set;}
        public int userId {get; set;}
        public User user {get; set;}
        public int offerId {get; set;}
        public Offer offer {get; set;}
        public string comment {get; set;}
        public int starRate { get; set; }
    }
}