using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IResevationPreviewService
    {
        Task<Model.model.ReservationPreview> GeneratePreviewAsync(ReservationPreviewRequest request);

        Task<bool> ApprovingRatePayment(int hotelId);
    }
}