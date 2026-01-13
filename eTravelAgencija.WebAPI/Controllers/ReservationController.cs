using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Models.Requests;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class ReservationController : BaseCRUDController<Model.model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>
    {
        public ReservationController(ILogger<BaseCRUDController<Model.model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>> logger, IReservationService reservationService) : base(logger, reservationService)
        {

        }

        [HttpPost("check-active")]
        public async Task<IActionResult> CheckActiveReservations()
        {
            await (_service as IReservationService).CheckAllReservationsActive();
            return Ok("Reservation statuses updated.");
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateBill([FromBody] BillRequest request)
        {
            var pdfBytes = await (_service as IReservationService).GenerateBillPdf(request);

            return File(
                pdfBytes,
                "application/pdf",
                $"racun_{request.ReservationId}.pdf"
            );
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmReservation(
            ConfirmReservationRequest request)
        {
            await (_service as IReservationService).ConfirmReservationAsync(
                request.ReservationId,
                request.DepartureDate,
                request.ReturnDate
            );

            return Ok();
        }

        [HttpPost("{id:int}/cancel-email")]
        public async Task<IActionResult> SendCancelEmail(int id, [FromBody] CancelReservationEmailRequest req)
        {
            var ok = await (_service as IReservationService).CancelReservation(id, req.Email);
            if (!ok) return BadRequest("Ne mogu poslati email (rezervacija ili email ne postoji).");

            return Ok();
        }

    }
}