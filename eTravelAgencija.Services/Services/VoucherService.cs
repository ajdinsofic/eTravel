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
    public class VoucherService
    : BaseCRUDService<Model.model.Voucher, VoucherSearchObject, Database.Voucher, VoucherUpsertRequest, VoucherUpsertRequest>,
      IVoucherService
    {
        public VoucherService(eTravelAgencijaDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IQueryable<Voucher> ApplyFilter(
     IQueryable<Voucher> query,
     VoucherSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search.Code))
            {
                query = query.Where(v => v.VoucherCode == search.Code);
            }

            return base.ApplyFilter(query, search);
        }


    }
}