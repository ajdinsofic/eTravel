

using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IHotelRoomsService 
        : ICRUDService<Model.model.HotelRooms, BaseSearchObject, HotelRoomInsertRequest, HotelRoomUpdateRequest>
    {
       
    }
}
