using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IResevationPreviewService
    {
        Task<Model.model.ReservationPreview> GeneratePreviewAsync(ReservationPreviewRequest request);
    }
}