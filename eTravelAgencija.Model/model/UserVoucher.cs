using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class UserVoucher
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }

        public bool isUsed {get; set;}
    }
}