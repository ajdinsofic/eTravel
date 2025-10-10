using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : Controller
    {

        [HttpGet]
        public async Task<List<UserResponse>> GetAsync([FromQuery] OfferSearchObject? search = null)
        {
            
        }
    }
}