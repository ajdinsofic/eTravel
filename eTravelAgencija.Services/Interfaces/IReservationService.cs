using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;


namespace eTravelAgencija.Services.Interfaces
{
    public interface IReservationService : ICRUDService<Model.model.Reservation,ReservationSearchObject,ReservationUpsertRequest,ReservationUpsertRequest>
    {
        Task CheckAllReservationsActive();
    }
}