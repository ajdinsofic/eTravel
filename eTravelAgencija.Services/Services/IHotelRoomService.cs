// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using eTravelAgencija.Model.RequestObjects;
// using eTravelAgencija.Model.ResponseObjects;
// using eTravelAgencija.Services.Database;

// namespace eTravelAgencija.Services.Services
// {
//     public interface IHotelRoomsService
//     {
//         Task<List<HotelRoomResponse>> GetAllAsync();
//         Task<HotelRoomResponse> GetByKeyForUserAsync(int offerId, int hotelId, int roomId);
//         Task<HotelRoomResponse> GetByKeyForAdminAsync(int hotelId, int roomId);
//         Task<HotelRoomUpsertResponse> CreateAsync(HotelRoomRequest request);
//         Task<HotelRoomUpsertResponse> UpdateAsync(int hotelId, int roomId, HotelRoomRequest request);
//         Task<bool> DeleteAsync(int hotelId, int roomId);
//     }

// }

using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IHotelRoomsService 
        : ICRUDService<Model.model.HotelRooms, HotelRoomSearchObject, HotelRoomRequest, HotelRoomRequest>
    {
       
    }
}
