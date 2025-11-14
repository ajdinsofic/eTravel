using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class Comment
    {
        [Key]
        public int Id {get; set;}
        public int userId {get; set;}
        public User user {get; set;}
        public int offerId {get; set;}
        public Offer offer {get; set;}
        public string comment {get; set;}
        public int starRate { get; set; }

    }
}