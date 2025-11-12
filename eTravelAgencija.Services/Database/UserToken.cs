using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class UserToken
    {
        [Key]
        [ForeignKey(nameof(User))] 
        public int UserId { get; set; }

        public User User { get; set; }

        public int Equity { get; set; }
    }
}