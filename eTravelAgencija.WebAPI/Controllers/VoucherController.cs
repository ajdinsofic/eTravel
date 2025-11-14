using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class VoucherController : BaseCRUDController<Model.model.Voucher, VoucherSearchObject, VoucherUpsertRequest, VoucherUpsertRequest>
    {
        public VoucherController(ILogger<BaseCRUDController<Model.model.Voucher, VoucherSearchObject, VoucherUpsertRequest, VoucherUpsertRequest>> logger, IVoucherService voucherService): base(logger, voucherService)
        {
            
        }
    }
}