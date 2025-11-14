using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IVoucherService : ICRUDService<Model.model.Voucher, VoucherSearchObject,VoucherUpsertRequest, VoucherUpsertRequest>
    {
        
    }
}