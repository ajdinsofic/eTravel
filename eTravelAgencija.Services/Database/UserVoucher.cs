using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    // Za vaucere cemo gledati do kraja mjeseca da se iskoriste,
    // jer svakog prvog u mjesecu resetuju se vauceri
    public class UserVoucher
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Voucher))]
        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }
        public bool isUsed {get; set;}
    }
}