using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class VoucherService : BaseCRUDService<Model.model.Voucher, VoucherSearchObject,Database.Voucher,VoucherUpsertRequest,VoucherUpsertRequest>, IVoucherService
    {
        public VoucherService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Voucher> ApplyFilter(IQueryable<Voucher> query, VoucherSearchObject search)
        {
            query = query.Where(o => o.Id == search.VoucherId);

            return base.ApplyFilter(query, search);
        }

        public override async Task<IEnumerable<Voucher>> AfterGetAsync(IEnumerable<Voucher> entities, VoucherSearchObject? search = null)
        {
            var userToken = await _context.UserTokens.Include(t => t.User)
                .FirstOrDefaultAsync(t => t.UserId == search.UserId);

            if (userToken == null)
                throw new Exception("Korisnik nije pronađen.");

            var userTokens = userToken.Equity;
            if (userTokens <= 0)
                throw new Exception("Korisnik nema tokene.");

            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == search.VoucherId);
            if (voucher == null)
                throw new Exception("Vaučer nije pronađen.");

            if (search.NumberOfTokens >= voucher.priceInTokens)
            {

                var newUserVoucher = new UserVoucher
                {
                    UserId = search.UserId,
                    VoucherId = search.VoucherId
                };

                _context.UserVouchers.Add(newUserVoucher);

                userToken.Equity -= search.NumberOfTokens;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Korisnik nema dovoljno tokena za otključavanje vaučera.");
            }

            return entities;
        }
    }
}