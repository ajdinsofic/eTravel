using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IHotelService : ICRUDService<Model.model.Hotel, HotelSearchObject, HotelUpsertRequest, HotelUpsertRequest>
    {

    }
}