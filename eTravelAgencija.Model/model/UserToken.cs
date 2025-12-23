using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public class UserToken
    {
        public int UserId { get; set; }

        public int Equity { get; set; }
    }
}