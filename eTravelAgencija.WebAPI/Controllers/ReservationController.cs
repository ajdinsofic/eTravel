using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class ReservationController : BaseCRUDController<Model.model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>
    {
        public ReservationController(ILogger<BaseCRUDController<Model.model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>> logger,IReservationService reservationService):base(logger, reservationService)
        {
            
        }

        
    }
}