using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }

        public string VoucherCode { get; set; }

        // decimal iz razloga sto da ne povlacim stalno, te da ne racunam tipa 10 / 100 = 0,10 bolje odmah
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }
        public int priceInTokens { get; set; }
        public ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();

    }
}