using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public string VoucherCode { get; set; }
        public decimal Discount { get; set; }
        public int priceInTokens { get; set; }
        public ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
    }
}