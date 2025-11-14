using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class CommentUpsertRequest
    {
        public int userId {get; set;}
        public int offerId {get; set;}
        public string comment {get; set;}
        public int starRate { get; set; }
    }
}