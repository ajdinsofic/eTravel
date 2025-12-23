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
    public class PaymentController : BaseCRUDController<Model.model.Payment, PaymentSearchObject, PaymentInsertRequest, PaymentUpdateRequest>
    {
       public PaymentController(ILogger<BaseCRUDController<Model.model.Payment, PaymentSearchObject, PaymentInsertRequest, PaymentUpdateRequest>> logger, IPaymentService paymentService):base(logger, paymentService)
        {
            
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpGet("{reservationId}/{rateId}")]
        public async Task<Model.model.Payment?> GetByCompositeKey(int reservationId, int rateId)
        {
            return await _service.GetByCompositeKeysAsync(reservationId, rateId);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("{reservationId}/{rateId}")]
        public async Task<Model.model.Payment?> UpdateByCompositeKey(int reservationId, int rateId, PaymentUpdateRequest plan)
        {
            return await _service.UpdateCompositeAsync([reservationId,rateId], plan);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpDelete("{reservationId}/{rateId}")]
        public async Task<bool> DeleteByCompositeKey(int reservationId, int rateId)
        {
            return await _service.DeleteCompositeAsync(reservationId, rateId);
        }

        [HttpGet("summary/{reservationId}")]
        public async Task<ActionResult<Model.model.PaymentSummary>> GetSummary(int reservationId)
        {
            return await (_service as IPaymentService).GetPaymentSummary(reservationId);
        }
    }
}