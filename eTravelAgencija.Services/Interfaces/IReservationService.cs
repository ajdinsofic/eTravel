using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Models.Requests;


namespace eTravelAgencija.Services.Interfaces
{
    public interface IReservationService : ICRUDService<Model.model.Reservation,ReservationSearchObject,ReservationUpsertRequest,ReservationUpsertRequest>
    {
        Task CheckAllReservationsActive();

        Task<byte[]> GenerateBillPdf(BillRequest req);

        Task ConfirmReservationAsync(
          int reservationId,
          DateTime departureDate,
          DateTime returnDate);
    }
}