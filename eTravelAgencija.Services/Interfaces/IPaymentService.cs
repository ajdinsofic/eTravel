using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IPaymentService : ICRUDService<Model.model.Payment, PaymentSearchObject,PaymentInsertRequest,PaymentUpdateRequest>
    {
        Task<Model.model.PaymentSummary> GetPaymentSummary(int reservationId);
    }
}