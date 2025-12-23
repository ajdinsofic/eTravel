using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class HotelRoomController : BaseCRUDController<Model.model.HotelRooms, HotelRoomSearchObject, HotelRoomInsertRequest, HotelRoomUpdateRequest>
    {
        public HotelRoomController(ILogger<BaseCRUDController<Model.model.HotelRooms, HotelRoomSearchObject, HotelRoomInsertRequest, HotelRoomUpdateRequest>> logger, IHotelRoomsService hotelRoomsService) : base(logger, hotelRoomsService) { }

        [AllowAnonymous]
        [HttpGet("{hotelId}/{roomId}")]
        public async Task<Model.model.HotelRooms?> GetByCompositeKey(int hotelId, int roomId)
        {
            return await _service.GetByCompositeKeysAsync(hotelId, roomId);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("{hotelId}/{roomId}")]
        public async Task<Model.model.HotelRooms?> UpdateByCompositeKey(int hotelId, int roomId, HotelRoomUpdateRequest plan)
        {
            return await _service.UpdateCompositeAsync([hotelId, roomId], plan);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpDelete("{hotelId}/{roomId}")]
        public async Task<bool> DeleteByCompositeKey(int hotelId, int roomId)
        {
            return await _service.DeleteCompositeAsync(hotelId, roomId);
        }

        [HttpPut("increase-rooms-left")]
        public async Task<IActionResult> IncreaseRoomsLeft(
         [FromQuery] int hotelId,
         [FromQuery] int roomId)
        {
            await (_service as IHotelRoomsService).IncreaseRoomsLeft(hotelId, roomId);
            return Ok(true);
        }



    }
}