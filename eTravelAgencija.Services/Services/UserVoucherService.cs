using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class UserVoucherService
        : BaseService<Model.model.UserVoucher, UserVoucherSearchObject, Database.UserVoucher>,
          IUserVoucherService
    {
        public UserVoucherService(eTravelAgencijaDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IQueryable<UserVoucher> ApplyFilter(IQueryable<UserVoucher> query, UserVoucherSearchObject search)
        {
            if (search?.userId != null)
            {
                query = query.Where(x => x.UserId == search.userId);
            }
            return base.ApplyFilter(query, search);
        }

        public async Task BuyVoucherAsync(BuyVoucherRequest request)
        {
            var userToken = await _context.UserTokens
                .FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (userToken == null)
                throw new Exception("Korisnik nema token zapis.");

            var voucher = await _context.Vouchers
                .FirstOrDefaultAsync(v => v.Id == request.VoucherId);

            if (voucher == null)
                throw new Exception("Vaučer ne postoji.");

            if (userToken.Equity < voucher.priceInTokens)
                throw new Exception("Nedovoljan broj tokena.");

            bool alreadyHasVoucher = await _context.UserVouchers.AnyAsync(
                uv => uv.UserId == request.UserId && uv.VoucherId == request.VoucherId);

            if (alreadyHasVoucher)
                throw new Exception("Korisnik već posjeduje ovaj vaučer.");

            var userVoucher = new Database.UserVoucher
            {
                UserId = request.UserId,
                VoucherId = request.VoucherId
            };

            _context.UserVouchers.Add(userVoucher);

            userToken.Equity -= voucher.priceInTokens;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> MarkAsUsed(MarkVoucherUsedRequest request)
        {
            var userVoucher = await _context.UserVouchers
                .FirstOrDefaultAsync(x =>
                    x.UserId == request.UserId &&
                    x.VoucherId == request.VoucherId);

            if (userVoucher == null)
                throw new Exception("Vaučer nije pronađen.");

            if (userVoucher.isUsed)
                throw new Exception("Vaučer je već iskorišten.");

            userVoucher.isUsed = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
